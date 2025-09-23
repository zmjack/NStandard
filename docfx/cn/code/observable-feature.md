# Observable 特性 (预览)

<br/>

[!code-csharp[](../../../Tests/NStandard.Test/Analyzer/ObservableFeatureTests.cs#doc_ObservableFeature)]

分析器会在编译期自动收集依赖：

```mermaid
graph TB
classDef leaf fill:#fdc
subgraph Notification Model
n_FirstName[FirstName]:::leaf
n_LastName[LastName]:::leaf
n_HelloText(HelloText)
n_Group(FullName<br/>Description)
n_FirstName --> n_Group
n_LastName --> n_Group
n_FirstName --> n_HelloText
end
subgraph Reference Model
r_FirstName[FirstName]:::leaf
r_LastName[LastName]:::leaf
r_FullName(FullName)
r_HelloText(HelloText)
r_Description(Description)
r_FirstName --> r_FullName
r_FirstName --> r_HelloText
r_LastName --> r_FullName
r_FullName --> r_Description
end
```

生成的分部代码：

```csharp
public partial class ObservablePerson : INotifyPropertyChanging, INotifyPropertyChanged
{
    public event PropertyChangingEventHandler PropertyChanging;
    public event PropertyChangedEventHandler PropertyChanged;
    
    private string backing_FirstName;
    public partial string FirstName
    {
        get => backing_FirstName;
        set
        {
            if (!EqualityComparer<string>.Default.Equals(backing_FirstName, value))
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("FirstName"));
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("FullName"));
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("HelloText"));
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("Description"));
                backing_FirstName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FirstName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FullName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HelloText"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }
        }
    }
    private string backing_LastName;
    public partial string LastName
    {
        get => backing_LastName;
        set
        {
            if (!EqualityComparer<string>.Default.Equals(backing_LastName, value))
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("LastName"));
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("FullName"));
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("Description"));
                backing_LastName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FullName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }
        }
    }
}
```

相似的 MVVM 代码：

```csharp
public partial class ObservablePerson_Mvvm : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName), nameof(Description), nameof(HelloText))]
    public string firstName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName), nameof(Description))]
    public string lastName;

    public string FullName => $"{FirstName} {LastName}";
    public string HelloText => $"Hello {FirstName}";
    public string Description => $"This person's name is {FullName}.";
}
```

与使用 **NotifyPropertyChangedFor** 标记引用位置相比，此功能可以显著减少复杂引用场景下的代码量。

<br/>

## 依赖收集

依赖收集范围仅限于 **属性**，包括以下区域：

- 箭头语法
- **Getter** 区域。

分析器将解析以下语法，以收集依赖：

- 函数调用参数
- 字符串插值
- 前缀一元运算符
- 二元运行符
- 三目运算
- 条件运算符
- 括号运算符

<br/>

### 另一个例子

###### [KDA 分数](#tab/kda)

[!code-csharp[](../../../Tests/NStandard.Test/Analyzer/ObservableFeatureTests.cs#doc_KDA)]

###### [生成部分](#tab/KDA-g)

```csharp
public partial class KDA : INotifyPropertyChanging, INotifyPropertyChanged
{
    public event PropertyChangingEventHandler PropertyChanging;
    public event PropertyChangedEventHandler PropertyChanged;
    
    private int backing_Kills;
    public partial int Kills
    {
        get => backing_Kills;
        set
        {
            if (!EqualityComparer<int>.Default.Equals(backing_Kills, value))
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("Kills"));
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("Score"));
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("PanelColor"));
                backing_Kills = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Kills"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Score"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PanelColor"));
            }
        }
    }
    private int backing_Deaths;
    public partial int Deaths
    {
        get => backing_Deaths;
        set
        {
            if (!EqualityComparer<int>.Default.Equals(backing_Deaths, value))
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("Deaths"));
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("Score"));
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("PanelColor"));
                backing_Deaths = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Deaths"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Score"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PanelColor"));
            }
        }
    }
    private int backing_Assists;
    public partial int Assists
    {
        get => backing_Assists;
        set
        {
            if (!EqualityComparer<int>.Default.Equals(backing_Assists, value))
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("Assists"));
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("Score"));
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("PanelColor"));
                backing_Assists = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Assists"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Score"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PanelColor"));
            }
        }
    }
}
```

---

