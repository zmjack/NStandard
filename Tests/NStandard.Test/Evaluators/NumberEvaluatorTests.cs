using Xunit;

namespace NStandard.Evaluators.Test;

public class NumberEvaluatorTests
{
    private class Item
    {
        public double Price { get; set; }
    }

    public class MyEvaluator : NumericalEvaluator
    {
        public MyEvaluator()
        {
            Define("!", value => value != 0d ? 0d : 1d);
            DefineBracket(new("|", "|"), Math.Abs);
            Initialize();
        }
    }

    [Fact]
    public void MyEvaluatorTest1()
    {
        var evaluator = new MyEvaluator();
        var func = evaluator.Compile("|-9| + not ${x} ? 3 : 2");
        var actual = func(new
        {
            x = -1,
        });
        Assert.Equal(3, actual);
    }

    [Fact]
    public void MyEvaluatorTest2()
    {
        var evaluator = new MyEvaluator();
        var actual = evaluator.Eval("|-9| + !0");
        Assert.Equal(10, actual);
    }

    [Fact]
    public void TypicalTest()
    {
        var exp = "${x} + sqrt(abs(${x} * 3)) * 3";
        var func = Evaluator.Numerical.Compile(exp);

        Assert.Equal(6, func(new { x = -3 }));
        Assert.Equal(6, func(new Dictionary<string, double> { ["x"] = -3 }));
    }

    [Fact]
    public void UnaryMinusOperatorTest()
    {
        var exp = "-${x}";
        var func = Evaluator.Numerical.Compile(exp);
        Assert.Equal(-3, func(new { x = 3 }));
    }

    [Fact]
    public void ModelTest()
    {
        var func = Evaluator.Numerical.Compile<Item>("${Price} >= 100 ? ${Price} * 0.8 : ${Price}");
        var actual = func(new Item { Price = 100 });
        Assert.Equal(80, actual);
    }

    [Fact]
    public void ComplexTest()
    {
        var exp = "(((3 and 4 or 5) == 5 ? 0 : 3 ** 2) + 3 * 2 / 3) // 2 % 3";
        var actual = Evaluator.Numerical.Eval(exp);
        Assert.Equal(2, actual, 2);
    }

    [Fact]
    public void NormalTest0()
    {
        var exp = "0.8";
        var actual = Evaluator.Numerical.Eval(exp);
        Assert.Equal(0.8, actual);
    }

    [Fact]
    public void NormalTest1()
    {
        var exp = "(20120416)";
        var actual = Evaluator.Numerical.Eval(exp);
        Assert.Equal(20120416, actual);
    }

    [Fact]
    public void NormalTest2()
    {
        double excepted = 1 + (2 * 3 - 4 * (5 + 6)) + 7;
        var exp = "1 + (2 * 3 - 4 * (5 + 6)) + 7";
        var actual = Evaluator.Numerical.Eval(exp);
        Assert.Equal(excepted, actual);
    }

    [Fact]
    public void NormalTest3()
    {
        double excepted = 3 + 5 * 8.1 - 6;
        var exp = "3 + 5 * 8.1 - 6";
        var actual = Evaluator.Numerical.Eval(exp);
        Assert.Equal(excepted, actual);
    }

    [Fact]
    public void TernaryTest()
    {
        double excepted = 1 >= 2 ? 3 : 4;
        var exp = "1 >= 2 ? 3 : 4";
        var actual = Evaluator.Numerical.Eval(exp);
        Assert.Equal(excepted, actual);
    }

    [Fact]
    public void NormalParameterTest()
    {
        var exp = "1 + (2 * ${a} - 4 * (${b} + 6)) + 7";
        var tree = Evaluator.Numerical.GetExpression(exp, out _);
        Assert.Equal("((1 + ((2 * IIF(p.ContainsKey(\"a\"), p.get_Item(\"a\"), 0)) - (4 * (IIF(p.ContainsKey(\"b\"), p.get_Item(\"b\"), 0) + 6)))) + 7)", tree.ToString());

        var del = Evaluator.Numerical.Compile(exp);
        Assert.Equal(1 + (2 * 3 - 4 * (5 + 6)) + 7, del(new Dictionary<string, double> { ["a"] = 3, ["b"] = 5 }));
        Assert.Equal(1 + (2 * 4 - 4 * (6 + 6)) + 7, del(new Dictionary<string, double> { ["a"] = 4, ["b"] = 6 }));
        Assert.Equal(1 + (2 * 4 - 4 * (0 + 6)) + 7, del(new Dictionary<string, double> { ["a"] = 4 }));
    }

    [Fact]
    public void ArgumentNumberErrorTest()
    {
        var exp = "$a + $b";
        Assert.Throws<ArgumentException>(() => Evaluator.Numerical.Eval(exp));
    }

    [Fact]
    public void InvalidExpStringErrorTest()
    {
        var exp = "1 # 2";
        Assert.Throws<ArgumentException>(() => Evaluator.Numerical.Eval(exp));
    }

    [Fact]
    public void UnopenedBracketErrorTest()
    {
        var exp = "1 + 2 + (3 + 4))";
        Assert.Throws<ArgumentException>(() => Evaluator.Numerical.Eval(exp));
    }

    [Fact]
    public void UnclosedBracketErrorTest()
    {
        var exp = "1 + (2 + (3 + 4)";
        Assert.Throws<ArgumentException>(() => Evaluator.Numerical.Eval(exp));
    }

}
