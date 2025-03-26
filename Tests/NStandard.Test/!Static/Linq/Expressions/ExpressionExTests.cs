using NStandard.Static.Linq.Expressions;
using System.Linq.Expressions;
using Xunit;
namespace NStandard.Test;

public class ExpressionExTests
{
    public class Model
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    [Fact]
    public void Test1()
    {
        var model = new Model { Name = "A" };
        Expression<Func<Model, int>> exp = x => x.Value;
        var setter = ExpressionEx.GetSetterExpression(exp).Compile();
        setter(model, 100);
        Assert.Equal(100, model.Value);
    }
}

