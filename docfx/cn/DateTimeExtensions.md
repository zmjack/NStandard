## DateTime Extensions

### StartOf & EndOf

提供 **DateTime** / **DateTimeOffset** 将时间返回到起点或终点的操作。

**StartOf**  函数组:

| Name              | Example                                        | Result                                         |
| ----------------- | ---------------------------------------------- | ---------------------------------------------- |
| **StartOfYear**   | (2020, 11, 13, 20, 30, 1) .**StartOfYear**()   | 2020/<font color=red>1/1 0:00:00:000</font>    |
| **StartOfMonth**  | (2020, 11, 13, 20, 30, 1) .**StartOfMonth**()  | 2020/11/<font color=red>1 0:00:00:000</font>   |
| **StartOfDay**    | (2020, 11, 13, 20, 30, 1) .**StartOfDay**()    | 2020/11/13 <font color=red>0:00:00:000</font>  |
| **StartOfHour**   | (2020, 11, 13, 20, 30, 1) .**StartOfHour**()   | 2020/11/13 20:<font color=red>00:00:000</font> |
| **StartOfMinute** | (2020, 11, 13, 20, 30, 1) .**StartOfMinute**() | 2020/11/13 20:30:<font color=red>00:000</font> |
| **StartOfSecond** | (2020, 11, 13, 20, 30, 1) .**StartOfSecond**() | 2020/11/13 20:30:01:<font color=red>000</font> |

**EndOf**  函数组:

| Name            | Example                                      | Result                                         |
| --------------- | -------------------------------------------- | ---------------------------------------------- |
| **EndOfYear**   | (2020, 11, 13, 20, 30, 1) .**EndOfYear**()   | 2020/<font color=red>12/31 23:59:59:999</font> |
| **EndOfMonth**  | (2020, 11, 13, 20, 30, 1) .**EndOfMonth**()  | 2020/11/<font color=red>30 23:59:59:999</font> |
| **EndOfDay**    | (2020, 11, 13, 20, 30, 1) .**EndOfDay**()    | 2020/11/13 <font color=red>23:59:59:999</font> |
| **EndOfHour**   | (2020, 11, 13, 20, 30, 1) .**EndOfHour**()   | 2020/11/13 20:<font color=red>59:59:999</font> |
| **EndOfMinute** | (2020, 11, 13, 20, 30, 1) .**EndOfMinute**() | 2020/11/13 20:30:<font color=red>59:999</font> |
| **EndOfSecond** | (2020, 11, 13, 20, 30, 1) .**EndOfSecond**() | 2020/11/13 20:30:01:<font color=red>999</font> |

<br/>

### Week

获取 **一年中** 指定日期的周数。

```csharp
/*  2020 - 01
 *  Su  Mo  Tu  We  Th  Fr  Sa
 *[            ( 1)  2   3   4 ]
 *[( 5)  6   7   8   9  10  11 ]
 *  12  13  14  15  16  17  18
 *  19  20  21  22  23  24  25
 *  26  27  28  29  30  31
 */
```

| Example                  | Result |
| ------------------------ | ------ |
| (2020, 1, 1) .**Week**() | 0      |
| (2020, 1, 5) .**Week**() | 1      |

<br/>

### WeekInMonth

获取指定日期 **一个月** 中的周数。

```csharp
/*  2012 - 04                              2012 - 04
 *  Su  Mo  Tu  We  Th   Fr  Sa            Su  Mo  Tu  We  Th  Fr  Sa
 *                      [     1          [                          1]
 *   2   3   4   5   6] [ 7   8          [  2   3   4   5   6   7   8]
 *   9  10  11  12  13] [14  15          [  9  10  11  12  13  14  15]
 * (16) 17  18  19  20] [21  22          [(16) 17  18  19  20  21  22]
 *  23  24  25  26  27   28  29            23  24  25  26  27  28  29
 *  30                                    30
 */
```

| Example                                                 | Result |
| ------------------------------------------------------- | ------ |
| (2012, 4, 16) .**WeekInMonth** ( **DayOfWeek.Friday** ) | 2      |
| (2012, 4, 16) .**WeekInMonth** ( **DayOfWeek.Sunday** ) | 3      |

<br/>

### AddDays

提供计算 **工作日** 或 **非工作日** 的特殊算术方法。

```csharp
public static DateTime AddDays(this DateTime @this, int value, DayMode mode)
```

**DayMode** 声明为：

```csharp
public enum DayMode {
    Undefined,
    Weekday,
    Weekend,
}
```

```csharp
/*  2020 - 12                             2020 - 12
 *  Su  Mo  Tu  We  Th  Fr  Sa            Su  Mo  Tu  We  Th  Fr  Sa
 *           1   2   3   4   5
 *   6   7   8   9  10  11  12
 * (13) 14  15  16  17  18 [19]
 *  20 [21] 22  23  24  25  26
 *  27  28  29  30  31
 */
```

| Example                                                | Result         |
| ------------------------------------------------------ | -------------- |
| (2020, 12, 13) .**AddDays** ( 6, **DayMode.Weekday** ) | (2020, 12, 21) |
| (2020, 12, 13) .**AddDays** ( 1, **DayMode.Weekend** ) | (2020, 12, 19) |

<br/>

### AddTotalYears & AddTotalMonths

- **AddTotalYears** ：返回一个新的 **DateTime**，它将指定的 **年数差值** 相加到此实例值中。
- **AddTotalMonths** : 返回一个新的 **DateTime** ，它将指定的 **月数差值** 相加到此实例值中。

| Example                                | Result       |
| -------------------------------------- | ------------ |
| (2000, 2, 29) .**AddTotalYears** ( 1 ) | (2001, 3, 1) |
| (2001, 3, 1) .**AddTotalYears** ( -1 ) | (2000, 3, 1) |

<br/>

### Elapsed...

从 **DateTime.MinValue** 获取经过 ... 的单位次数。

| Example                              | Result  |
| ------------------------------------ | ------- |
| (2012, 4, 16) .**ElapsedDays** ( )   | 734608  |
| (2012, 4, 16) .**ElapsedMonths** ( ) | 24135.5 |

<br/>

