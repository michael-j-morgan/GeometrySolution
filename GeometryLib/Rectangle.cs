namespace GeometryLib;

public class Rectangle : IShape
{
    public string Name { get; } = "Rectangle";
    public double Width { get; }
    public double Height { get; }

    public Rectangle(double width, double height)
    {
        if (width <= 0 || height <= 0)
            throw new ArgumentException("Sides must be positive.");

        Width = width;
        Height = height;
    }

    public double Area() => Width * Height;
    public double Perimeter() => 2 * (Width + Height);
}
