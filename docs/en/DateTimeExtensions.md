# NStandard

**.NET** extension library for system library.

<br/>

## XDateTime / XDateTimeOffset

Extensions for **DateTime**.

<br/>

### StartOf / EndOf

The **StartOf** and **EndOf** functions provide operations for the **DateTime** / **XDateTimeOffset** to return time to a starting point or advance to an end point.

There are the **StartOf**  functions:

| Name              | Example                                                | Result                  |
| ----------------- | ------------------------------------------------------ | ----------------------- |
| **StartOfYear**   | new DateTime(2020, 11, 13, 20, 30, 01).StartOfYear()   | 2020/1/1 0:00:00:000    |
| **StartOfMonth**  | new DateTime(2020, 11, 13, 20, 30, 01).StartOfMonth()  | 2020/11/1 0:00:00:000   |
| **StartOfDay**    | new DateTime(2020, 11, 13, 20, 30, 01).StartOfDay()    | 2020/11/13 0:00:00:000  |
| **StartOfHour**   | new DateTime(2020, 11, 13, 20, 30, 01).StartOfHour()   | 2020/11/13 20:00:00:000 |
| **StartOfMinute** | new DateTime(2020, 11, 13, 20, 30, 01).StartOfMinute() | 2020/11/13 20:30:00:000 |
| **StartOfSecond** | new DateTime(2020, 11, 13, 20, 30, 01).StartOfSecond() | 2020/11/13 20:30:01:000 |

There are the **EndOf**  functions:

| Name            | Example                                              | Result                  |
| --------------- | ---------------------------------------------------- | ----------------------- |
| **EndOfYear**   | new DateTime(2020, 11, 13, 20, 30, 01).EndOfYear()   | 2020/12/31 23:59:59:999 |
| **EndOfMonth**  | new DateTime(2020, 11, 13, 20, 30, 01).EndOfMonth()  | 2020/11/30 23:59:59:999 |
| **EndOfDay**    | new DateTime(2020, 11, 13, 20, 30, 01).EndOfDay()    | 2020/11/13 23:59:59:999 |
| **EndOfHour**   | new DateTime(2020, 11, 13, 20, 30, 01).EndOfHour()   | 2020/11/13 20:59:59:999 |
| **EndOfMinute** | new DateTime(2020, 11, 13, 20, 30, 01).EndOfMinute() | 2020/11/13 20:30:59:999 |
| **EndOfSecond** | new DateTime(2020, 11, 13, 20, 30, 01).EndOfSecond() | 2020/11/13 20:30:01:999 |

<br/>

### Week

Gets the number of weeks in a year for the specified date.

The table below demonstrates how to calculate the week number of **DateTime**.

```csharp
/*  2020 - 01
 *  Su  Mo  Tu  We  Th  Fr  Sa
 *             ( 1)  2   3   4
 * ( 5)  6   7   8   9  10  11
 *  12 (13) 14  15  16  17  18
 *  19  20  21  22  23  24 (25)
 *  26  27  28  29  30  31
 */
```

| Example                          | Result |
| -------------------------------- | ------ |
| new DateTime(2020, 1, 1).Week()  | 0      |
| new DateTime(2020, 1, 5).Week()  | 1      |
| new DateTime(2020, 1, 13).Week() | 2      |
| new DateTime(2020, 1, 25).Week() | 3      |

<br/>

### AddDays

This **AddDays** function is different from the system function **AddDays**. It provides a special arithmetic method for calculating working days or non-working days.

There is the declaration:

```csharp
public static DateTime AddDays(this DateTime @this, int value, DayMode mode)
```

And the **DayMode** is declared as:

```csharp
public enum DayMode {
    Undefined,
    Weekday,
    Weekend,
}
```

The table below demonstrates how to calculate the **DateTime** with **Weekdays** / **Weekends**.

```csharp
/*  2020 - 12
 *  Su  Mo  Tu  We  Th  Fr  Sa
 *           1   2   3   4   5
 *   6   7   8   9  10  11  12
 * (13) 14  15  16  17  18  19
 *  20  21  22  23  24  25  26
 *  27  28  29  30  31
 */
```

- **Weekday**

  | Example                                                 | Result     |
  | ------------------------------------------------------- | ---------- |
  | new DateTime(2020, 12, 13).AddDays(0, DayMode.Weekday)  | 2020/12/13 |
  | new DateTime(2020, 12, 13).AddDays(1, DayMode.Weekday)  | 2020/12/14 |
  | new DateTime(2020, 12, 13).AddDays(6, DayMode.Weekday)  | 2020/12/21 |
  | new DateTime(2020, 12, 13).AddDays(-1, DayMode.Weekday) | 2020/12/11 |
  | new DateTime(2020, 12, 13).AddDays(-6, DayMode.Weekday) | 2020/12/4  |

- **Weekend**

  | Example                                                 | Result     |
  | ------------------------------------------------------- | ---------- |
  | new DateTime(2020, 12, 13).AddDays(0, DayMode.Weekend)  | 2020/12/13 |
  | new DateTime(2020, 12, 13).AddDays(1, DayMode.Weekend)  | 2020/12/19 |
  | new DateTime(2020, 12, 13).AddDays(3, DayMode.Weekend)  | 2020/12/26 |
  | new DateTime(2020, 12, 13).AddDays(-1, DayMode.Weekend) | 2020/12/12 |
  | new DateTime(2020, 12, 13).AddDays(-3, DayMode.Weekend) | 2020/12/5  |

<br/>

### AddDiff

```csharp
//TODO
```

