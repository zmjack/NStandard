using NStandard.ComponentModel;
using System.Text;
using Xunit;

namespace NStandard.Analyzer.Test;

[ObservableFeature]
public partial class ObservableModel
{
    public partial string FirstName { get; set; }
    public partial string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}

public class ObservableFeatureTests
{
    [Fact]
    public void Test()
    {
        var sb = new StringBuilder();
        var observable = new ObservableModel();
        observable.PropertyChanging += (s, e) => sb.AppendLine($"Changing: {e.PropertyName}");
        observable.PropertyChanged += (s, e) => sb.AppendLine($"Changed: {e.PropertyName}");
        observable.FirstName = "Bill";
        observable.LastName = "Gates";
    }
}
