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
                if (tabletReport.Pressure < Pressure_threshold)
                {
                    if (debounceStopwatch.Elapsed.TotalMilliseconds <= Debounce_timer)
                    {
                        tabletReport.Pressure = last_pressure;
                        return input;
                    }
                    else
                    {
                        debounceStopwatch.Restart();
                        last_pressure = tabletReport.Pressure;
                        return input;
                    }
                }
                else
                {
                    debounceStopwatch.Restart();
                    last_pressure = tabletReport.Pressure;
                    return input;
                }
            }
            else
            {
                return input;
            }
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

        [Property("Debounce Timer"), Unit("ms"), DefaultPropertyValue(30f)]
        public float Debounce_timer { set; get; }

        [Property("Pressure Threshold"), DefaultPropertyValue(1f)]
        public float Pressure_threshold { set; get; }
    }
}