using Xunit;
using GeometryLib;   // 👈 Makes Circle visible to this test project

namespace GeometryTests;

public class CircleTest
{
    [Fact]
    public void Circle_Area_WithRadius5_ReturnsCorrectValue()
    {
        // Arrange
        var circle = new Circle(5);

        // Act
        var result = circle.Area();

        // Assert
        Assert.Equal(Math.PI * 25, result, precision: 3);
    }

    [Fact]
    public void Circle_Perimeter_WithRadius5_ReturnsCorrectValue()
    {
        // Arrange
        var circle = new Circle(5);

        // Act
        var result = circle.Perimeter();

        // Assert
        Assert.Equal(2 * Math.PI * 5, result, precision: 3);
    }
}

