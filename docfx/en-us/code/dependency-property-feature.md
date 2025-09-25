# DependencyProperty Feature

<br/>

```csharp
/// <summary>
/// Interaction logic for Hexagon.xaml
/// </summary>
[DependencyPropertyFeature]
public partial class Hexagon : UserControl
{
    [DependencyProperty] public partial Brush Stroke { get; set; }
    [DependencyProperty] public partial Brush Fill { get; set; }
    [DependencyProperty] public partial Geometry Data { get; set; }

    [DependencyProperty]
    public partial int FlatTop { get; set; }
    public static void FlatTop_OnChanged(Hexagon hexagon, int value)
    {
        hexagon.Data = Geometry.Parse("M60,0 L180,0 240,104 180,208 60,208 0,104 Z");
        hexagon.Width = value;
        hexagon.Height = value * 208 / 240;
    }

    [DependencyProperty]
    public partial int PointyTop { get; set; }
    public static void PointyTop_OnChanged(Hexagon hexagon, int value)
    {
        hexagon.Data = Geometry.Parse("M104,0 L208,60 208,180 104,240 0,180 0,60 Z");
        hexagon.Width = value * 208 / 240;
        hexagon.Height = value;
    }

    public Hexagon()
    {
        InitializeComponent();
    }
}
```



