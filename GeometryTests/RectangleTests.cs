using Xunit;
using GeometryLib;

namespace GeometryTests;

public class RectangleTests
{
    [Fact]
    public void Rectangle_Area_ComputesCorrectly()
    {
        var rect = new Rectangle(4, 6);
        Assert.Equal(24, rect.Area());
    }

    [Fact]
    public void Rectangle_Perimeter_ComputesCorrectly()
    {
        var rect = new Rectangle(4, 6);
        Assert.Equal(20, rect.Perimeter());
    }

    [Fact]
    public void Rectangle_WithNegativeSide_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Rectangle(-4, 5));
    }
}
