using OpenTabletDriver.Plugin.Attributes;
using OpenTabletDriver.Plugin.Tablet;
using OpenTabletDriver.Plugin.Output;
using System;
using OpenTabletDriver.Plugin.Timing;

namespace Pressure_Debounce
{
    [PluginName("Pressure Debounce")]
    public class Pressure_Debounce : IPositionedPipelineElement<IDeviceReport>
    {
        protected HPETDeltaStopwatch debounceStopwatch = new HPETDeltaStopwatch(true);
        uint last_pressure = 0;
        public IDeviceReport Debounce(IDeviceReport input)
        {
            if (input is ITabletReport tabletReport)
            {
                if (Drop_excess)
                {
                    return Drop_Excess_Method(tabletReport);
                }
                return Default_Method(tabletReport);
            }
            return input;
        }

        public ITabletReport Default_Method(ITabletReport tabletReport)
        {
            if (tabletReport.Pressure < Pressure_threshold)
            {
                if (debounceStopwatch.Elapsed.TotalMilliseconds <= Debounce_timer)
                {
                    tabletReport.Pressure = last_pressure;
                    return tabletReport;
                }
                debounceStopwatch.Restart();
                last_pressure = tabletReport.Pressure;
                return tabletReport;
            }
            if (!Disable_timer)
            {
                debounceStopwatch.Restart();
            }
            last_pressure = tabletReport.Pressure;
            return tabletReport;
        }

        public ITabletReport Drop_Excess_Method(ITabletReport tabletReport)
        {
            if (tabletReport.Pressure > Pressure_threshold)
            {
                if (debounceStopwatch.Elapsed.TotalMilliseconds <= Debounce_timer)
                {
                    if (last_pressure < Pressure_threshold)
                    {
                        if (!Disable_timer)
                        {
                            debounceStopwatch.Restart();
                        }
                        tabletReport.Pressure = 0;
                    }
                    return tabletReport;
                }
                last_pressure = tabletReport.Pressure;
                debounceStopwatch.Restart();
                return tabletReport;
            }
            last_pressure = tabletReport.Pressure;
            return tabletReport;
        }

        public event Action<IDeviceReport> Emit;

        public void Consume(IDeviceReport value)
        {
            if (value is ITabletReport report)
            {
                report = (ITabletReport)Filter(report);
                value = report;
            }

            Emit?.Invoke(value);
        }

        public IDeviceReport Filter(IDeviceReport input) => Debounce(input);

        public PipelinePosition Position => PipelinePosition.PostTransform;

        [Property("Debounce Timer"), Unit("ms"), DefaultPropertyValue(30f), ToolTip
            ("Pressure Debounce:\n\n" +
            "Debounce Timer: The time in ms after input pressure goes below the Pressure Threshold and subsequent inputs are extended, combined, or dropped.")]
        public float Debounce_timer { set; get; }

        [Property("Pressure Threshold"), DefaultPropertyValue(1f), ToolTip
            ("Pressure Debounce:\n\n" +
            "Pressure Threshold: The raw pen pressure value at which debounce is applied.")]
        public float Pressure_threshold { set; get; }

        [BooleanProperty("Drop Excess Inputs", ""), ToolTip
            ("Pressure Debounce:\n\n" +
            "Drop Excess Inputs: Instead of extending or combining subsequent inputs, all subsequent inputs above the Pressure Threshold and within the time set in the Debounce Timer are dropped.")]
        public bool Drop_excess { set; get; }

        [BooleanProperty("Disable Timer Repeating", ""), ToolTip
        ("Pressure Debounce:\n\n" +
        "Disable Timer Repeating: The timer is not reset for each subsequent press within the time set in the Debounce Timer.")]
        public bool Disable_timer { set; get; }
    }
}