namespace GeometryLib;

public class Trapezoid : IShape
{
    public string Name { get; } = "Trapezoid";
    public double Base1 { get; }
    public double Base2 { get; }
    public double Height { get; }
    public double? Side1 { get; }
    public double? Side2 { get; }

    // Overload for area-only trapezoid
    public Trapezoid(double base1, double base2, double height)
    {
        if (base1 <= 0 || base2 <= 0 || height <= 0)
            throw new ArgumentException("All dimensions must be positive.");

        Base1 = base1;
        Base2 = base2;
        Height = height;
    }

    // Overload for full perimeter calculation
    public Trapezoid(double base1, double base2, double height, double side1, double side2)
        : this(base1, base2, height)
    {
        if (side1 <= 0 || side2 <= 0)
            throw new ArgumentException("Sides must be positive.");

        Side1 = side1;
        Side2 = side2;
    }

    public double Area() => ((Base1 + Base2) / 2) * Height;

    public double Perimeter()
    {
        if (Side1 == null || Side2 == null)
            throw new InvalidOperationException("Cannot calculate perimeter without side lengths.");

        return Base1 + Base2 + Side1.Value + Side2.Value;
    }
}