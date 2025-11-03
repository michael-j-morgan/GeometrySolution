using Xunit;
using GeometryLib;

namespace GeometryTests;

public class ParallelogramTests
{
    [Fact]
    public void Parallelogram_Area_ComputesCorrectly()
    {
        var p = new Parallelogram(6, 5, 4);
        Assert.Equal(24, p.Area());
    }

    [Fact]
    public void Parallelogram_Perimeter_ComputesCorrectly()
    {
        var p = new Parallelogram(6, 5, 4);
        Assert.Equal(22, p.Perimeter());
    }

    [Fact]
    public void Parallelogram_InvalidSides_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Parallelogram(-6, 4, 5));
    }
}
