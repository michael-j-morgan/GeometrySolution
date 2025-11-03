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

    public double Area()
    {
        // standard trapezoid area formula
        return 0.5 * (Base1 + Base2) * Height;
    }

    public double Perimeter()
    {
        // if sides are optional
        if (Side1.HasValue && Side2.HasValue)
            return Base1 + Base2 + Side1.Value + Side2.Value;

        // otherwise, you can either throw or return NaN — choose one strategy
        return double.NaN;
    }

    public Trapezoid(double base1, double base2, double height, double? side1 = null, double? side2 = null)
    {
        if (base1 <= 0) throw new ArgumentException("Base1 must be positive.", nameof(base1));
        if (base2 <= 0) throw new ArgumentException("Base2 must be positive.", nameof(base2));
        if (height <= 0) throw new ArgumentException("Height must be positive.", nameof(height));
        if (side1.HasValue && side1.Value <= 0)
            throw new ArgumentException("Side1 must be positive when provided.", nameof(side1));
        if (side2.HasValue && side2.Value <= 0)
            throw new ArgumentException("Side2 must be positive when provided.", nameof(side2));

        Base1 = base1; Base2 = base2; Height = height; Side1 = side1; Side2 = side2;
    }

}