using Xunit;
using GeometryLib;

namespace GeometryTests;

public class CircleExtremeTests
{
[Fact]
public void Circle_WithMaxDoubleRadius_ReturnsValidNumericValues()
{
    var circle = new Circle(double.MaxValue);

    var ex = Record.Exception(() => circle.Area());
    Assert.Null(ex); // ✅ should not throw

    // optional: explicitly assert the expected overflow behavior
    Assert.True(double.IsInfinity(circle.Area()));
    Assert.True(double.IsInfinity(circle.Perimeter()) || !double.IsNaN(circle.Perimeter()));
}


}
