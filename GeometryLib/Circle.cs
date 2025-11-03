namespace GeometryLib;

public class Circle : IShape
{
    public string Name { get; } = "Circle";
    public double Radius { get; }

    public Circle(double radius)
    {
        if (radius <= 0)
            throw new ArgumentException("Radius must be positive.", nameof(radius));
        Radius = radius;
    }

    public double Area() => Math.PI * Radius * Radius;
    public double Perimeter() => 2 * Math.PI * Radius;
}

