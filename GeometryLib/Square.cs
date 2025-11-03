namespace GeometryLib;

public class Square : IShape
{
    public string Name { get; } = "Square";
    public double Side { get; }

    public Square(double side)
    {
        if (side < 0)
            throw new ArgumentException("Side length cannot be negative.");
        Side = side;
    }

    public double Area() => Side * Side;
    public double Perimeter() => 4 * Side;
}

