using Xunit;
using GeometryLib;

namespace GeometryTests;

public class CircleExtremeTests
{
    [Fact]
    public void Circle_WithMaxDoubleRadius_ReturnsValidNumericValues()
    {
        // Arrange
        var circle = new Circle(double.MaxValue);

        // Act
        var area = circle.Area();
        var perimeter = circle.Perimeter();

        // Assert
        // It’s okay if Infinity occurs (expected overflow); just ensure not NaN.
        Assert.False(double.IsNaN(area));
        Assert.False(double.IsNaN(perimeter));

        // Optional: log behavior for visibility
        Console.WriteLine($"Area: {area}, Perimeter: {perimeter}");
    }

}
