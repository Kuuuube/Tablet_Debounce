# Tablet Debounce Plugin for [OpenTabletDriver](https://github.com/OpenTabletDriver/OpenTabletDriver) [![](https://img.shields.io/github/downloads/Kuuuube/Tablet_Debounce/total.svg)](https://github.com/Kuuuube/Tablet_Debounce/releases/latest)

Prevents unintended repitition of pen tip inputs.

## Explanation of the Values:

**Debounce Timer:** The time in ms after input pressure goes below the **Pressure Threshold** and subsequent inputs are extended, combined, or dropped.

**Pressure Threshold:** The raw pen pressure value at which inputs are handled. 

- **Default Action:** Output pressure will be held at or above the set **Pressure Threshold** after an input goes below the **Pressure Threshold** until the time set in the **Debounce Timer** has passed. 

    Subsequent inputs above the **Pressure Threshold** within the time set in the **Debounce Timer** will be combined with the previous input and reset the **Debounce Timer** until the pressure is below the **Pressure Threshold**.

- **Drop Excess Inputs:** Output pressure will be held below the set **Pressure Threshold** after an input goes below the **Pressure Threshold** until the time set in the **Debounce Timer** has passed. 

    Subsequent inputs above the **Pressure Threshold** within the time set in the **Debounce Timer** will be dropped and will reset the **Debounce Timer** until the pressure is below the **Pressure Threshold**.

**Drop Excess Inputs:** Instead of extending or combining subsequent inputs, all subsequent inputs above the **Pressure Threshold** and within the time set in the **Debounce Timer** are dropped.

<br>

## Debounce Comparison:

![](https://raw.githubusercontent.com/Kuuuube/Tablet_Debounce/main/tablet_debounce_diagram.png)
