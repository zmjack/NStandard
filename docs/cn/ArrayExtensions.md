# NStandard

**.NET** extension library for system library.

<br/>

## ArrayExtensions

按指定路径依次获取元素。

<br/>

### 原型映射

### Map

映射原数组为新类型数组，且不改变整体结构。

**示例 1（ 1d - 1d ）**：

```csharp
var d1_d1 = new string[2][]
{
    new string[1] { "0" },
    new string[2] { "1", "2" },
};
var result = d1_d1.Map((string s) => int.Parse(s)) as int[][];

Assert.Equal(new[] { 0, 1, 2 }, Any.Flat<int>(result));
```

![image-20221227125854278](https://raw.githubusercontent.com/zmjack/NStandard/master/docs/images/image-20221227125854278.png)

<br/>

**示例 2（ 1d - 2d ）**：

```csharp
var d1_d2 = new string[2][,]
{
    new string[2, 1]
    {
        { "0" },
        { "1" },
    },
    new string[1, 2]
    {
        { "2", "3" },
    },
};
var result = d1_d2.Map((string s) => int.Parse(s)) as int[][,];

Assert.Equal(new[] { 0, 1, 2, 3 }, Any.Flat<int>(result));
```

![image-20221227130010987](https://raw.githubusercontent.com/zmjack/NStandard/master/docs/images/image-20221227130010987.png)

<br/>

**示例 3（ 1d - 2d - 1d ）**：

```csharp
var d1_d2_d1 = new string[2][,][]
{
    new string[1, 2][]
    {
        {
            new string [1] { "0", },
            new string [2] { "1", "2" },
        },
    },
    new string[2, 1][]
    {
        {
            new string [2] { "3", "4",},
        },
        {
            new string [3] { "5", "6", "7"},
        },
    },
};
var result = d1_d2_d1.Map((string s) => int.Parse(s)) as int[][,][];

Assert.Equal(new[] { 0, 1, 2, 3, 4, 5, 6, 7 }, Any.Flat<int>(result));
```

![image-20221227130051999](https://raw.githubusercontent.com/zmjack/NStandard/master/docs/images/image-20221227130051999.png)

<br/>