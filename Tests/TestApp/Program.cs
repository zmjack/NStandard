using NStandard;

namespace TestApp;

internal class Program
{
    static void Main(string[] args)
    {
        var distances = new m[1000].Let(i => 2);
        var total = (km)distances.QuickSum();
        Console.WriteLine(total);      // 2 km
    }
}
