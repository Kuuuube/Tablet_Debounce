# Tablet Debounce Plugin for [OpenTabletDriver](https://github.com/OpenTabletDriver/OpenTabletDriver) [![](https://img.shields.io/github/downloads/Kuuuube/Tablet_Debounce/total.svg)](https://github.com/Kuuuube/Tablet_Debounce/releases/latest)

Prevents unintended repitition of pen tip inputs.

## Pressure Debounce:

**Debounce Timer:** The time in ms after input pressure hits **zero** and subsequent inputs are extended, combined, or dropped.

- **Default Action:** Output pressure will be held above **zero** after an input hits **zero** until the time set in the **Debounce Timer** has passed.

    Subsequent inputs above **zero** within the time set in the **Debounce Timer** will be combined with the previous input and reset the **Debounce Timer** until the pressure stays at **zero**.

- **Drop Excess Inputs:** Output pressure will be held at **zero** after an input hits **zero** until the time set in the **Debounce Timer** has passed.

    Subsequent inputs above **zero** within the time set in the **Debounce Timer** will be dropped and will reset the **Debounce Timer** until the pressure stays at **zero**.

**Drop Excess Inputs:** Instead of extending or combining subsequent inputs, all subsequent inputs above **zero** and within the time set in the **Debounce Timer** are dropped.

**Disable Timer Repeating:** The timer is not reset for each subsequent press within the time set in the **Debounce Timer**.

## Debounce Comparison:

![](https://raw.githubusercontent.com/Kuuuube/Tablet_Debounce/main/tablet_debounce_diagram.png)
