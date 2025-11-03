using Xunit;
using GeometryLib;

namespace GeometryTests;

public class ShapeTests
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
    public void Triangle_Perimeter_ThreeFourFive_Returns12()
    {
        // Arrange
        var triangle = new Triangle(3, 4, 5);

        // Act
        var result = triangle.Perimeter();

        // Assert
        Assert.Equal(12, result);
    }

    [Fact]
    public void Square_Area_WithSide4_Returns16()
    {
        // Arrange
        var square = new Square(4);

        // Act
        var result = square.Area();

        // Assert
        Assert.Equal(16, result);
    }
}


