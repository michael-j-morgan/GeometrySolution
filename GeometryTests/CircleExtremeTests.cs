using Xunit;
using GeometryLib;

namespace GeometryTests;

public class CircleExtremeTests
{
    [Fact]
    public void Circle_WithMaxDoubleRadius_ReturnsValidNumericValues()
    {
        var originalOut = Console.Out;
        using var sw = new StringWriter();
        Console.SetOut(sw);

        var circle = new Circle(double.MaxValue);
        var area = circle.Area();
        var perimeter = circle.Perimeter();

        Console.SetOut(originalOut); // ✅ Restore before any Console.WriteLine
        Console.WriteLine($"Area: {area}, Perimeter: {perimeter}");

        Assert.False(double.IsInfinity(area));
        Assert.False(double.IsNaN(perimeter));
    }


}
