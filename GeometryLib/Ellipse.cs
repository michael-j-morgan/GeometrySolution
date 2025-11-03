namespace GeometryLib;

public class Ellipse : IShape
{
    public string Name { get; } = "Ellipse";
    public double MajorAxis { get; }
    public double MinorAxis { get; }

public Ellipse(double majorAxis, double minorAxis)
{
    if (majorAxis <= 0)
        throw new ArgumentException("Major axis must be positive.", nameof(majorAxis));
    if (minorAxis <= 0)
        throw new ArgumentException("Minor axis must be positive.", nameof(minorAxis));

    MajorAxis = majorAxis;
    MinorAxis = minorAxis;
}


    public double Area() => Math.PI * MajorAxis * MinorAxis;

    public double Perimeter() {
      double h = Math.Pow(MajorAxis - MinorAxis, 2) / Math.Pow(MajorAxis + MinorAxis, 2);
      return 
        Math.PI * (MajorAxis + MinorAxis) * (1 + (3 * h) / (10 + Math.Sqrt(4 -3 * h)));

    }
}

