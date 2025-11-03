using Xunit;
using GeometryLib;

namespace GeometryTests;

public class EllipseTest
{
    [Fact]
    public void Ellipse_Area_CalculatesCorrectly()
    {
        var ellipse = new Ellipse(5, 3);
        Assert.Equal(Math.PI * 5 * 3, ellipse.Area(), precision: 3);
    }

    [Fact]
    public void Ellipse_InvalidAxes_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Ellipse(-2, 3));
    }
}

