# Tablet Debounce Plugin for [OpenTabletDriver](https://github.com/OpenTabletDriver/OpenTabletDriver)

Prevents unintended repitition of pen tip inputs.

## Explanation of the Values:

**Debounce Timer:** The time after an input when subsequent inputs are handled.

**Pressure Threshold:** The raw pen pressure value at which inputs are handled.

**Drop Excess Inputs:** Instead of combining subsequent inputs within the debounce ranges, the inputs are dropped.

<br>

## Debounce Comparison:

![](https://raw.githubusercontent.com/Kuuuube/Tablet_Debounce/main/tablet_debounce_diagram.png)