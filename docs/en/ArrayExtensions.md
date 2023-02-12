## ArrayExtensions

### Prototype mapping

### Map

Map the original array to a new type array without changing the overall structure.

**Example 1 ( 1d - 1d )**

```csharp
var d1_d1 = new string[2][]
{
    new string[1] { "0" },
    new string[2] { "1", "2" },
};
var result = d1_d1.Map((string s) => int.Parse(s)) as int[][];

Assert.Equal(new[] { 0, 1, 2 }, Any.Flat<int>(result));
```

![image-20221227131745251](https://raw.githubusercontent.com/zmjack/NStandard/master/docs/images/image-20221227131745251.png)

<br/>

**Example 2 ( 1d - 2d )**

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

![image-20221227134222183](https://raw.githubusercontent.com/zmjack/NStandard/master/docs/images/image-20221227134222183.png)

<br/>

**Example 3 ( 1d - 2d - 1d )**

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

![image-20221227134321883](https://raw.githubusercontent.com/zmjack/NStandard/master/docs/images/image-20221227134321883.png)

<br/>