using NStandard.ComponentModel;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Xunit;

namespace NStandard.Analyzer.Test;

#region doc_ObservableFeature
[ObservableFeature]
public partial class ObservablePerson : INotifyPropertyChanging, INotifyPropertyChanged
{
    public event PropertyChangingEventHandler PropertyChanging;
    public event PropertyChangedEventHandler PropertyChanged;

    public partial string FirstName { get; set; }
    public partial string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}".Trim();
    public string HelloText => $"Hello {FirstName}";
    public string Description => $"The person's name is {FullName}.";
}
#endregion

#region doc_KDA
[ObservableFeature]
public partial class KDA : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public partial int Kills { get; set; }
    public partial int Deaths { get; set; }
    public partial int Assists { get; set; }

    public double Score => Math.Round(Kills - Deaths + (Assists / 2.5), 1);
    public Color PanelColor
    {
        get
        {
            if (Score >= 0) return Color.FromArgb(0x99, 0x66, 0xcc, 0x66);
            else return Color.FromArgb(0x99, 0xcc, 0x66, 0x66);
        }
    }
}
#endregion

public class ObservableFeatureTests
{
    [Fact]
    public void Test()
    {
        var sb = new StringBuilder();
        var observable = new ObservablePerson();
        var type = observable.GetType();
        observable.PropertyChanging += (s, e) =>
        {
            sb.AppendLine($"Changing: {e.PropertyName}");
        };
        observable.PropertyChanged += (s, e) =>
        {
            sb.AppendLine($"Changed: {e.PropertyName}");
            var value = type.GetProperty(e.PropertyName)?.GetValue(observable);
            sb.AppendLine($"<value>: {value}");
        };

        observable.FirstName = "Bill";
        Assert.Equal("""
        Changing: FirstName
        Changing: FullName
        Changing: HelloText
        Changing: Description
        Changed: FirstName
        <value>: Bill
        Changed: FullName
        <value>: Bill
        Changed: HelloText
        <value>: Hello Bill
        Changed: Description
        <value>: The person's name is Bill.
        
        """, sb.ToString());
        sb.Clear();

        observable.LastName = "Gates";
        Assert.Equal("""
        Changing: LastName
        Changing: FullName
        Changing: Description
        Changed: LastName
        <value>: Gates
        Changed: FullName
        <value>: Bill Gates
        Changed: Description
        <value>: The person's name is Bill Gates.

        """, sb.ToString());
    }
}
