namespace GeometryLib;

public class Parallelogram : IShape
{
    public string Name { get; } = "Parallelogram";
    public double Base { get; }
    public double Side { get; }
    public double Height { get; }

    public Parallelogram(double b, double height, double side)
    {
        if (b <= 0 || side <= 0 || height <= 0)
            throw new ArgumentException("Base, side, and height must be positive.");

        Base = b;
        Side = side;
        Height = height;
    }

    public double Area() => Base * Height;
    public double Perimeter() => 2 * (Base + Side);
}
