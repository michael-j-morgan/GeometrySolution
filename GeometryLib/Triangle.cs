namespace GeometryLib;

public class Triangle : IShape
{
    public string Name { get; } = "Triangle";
    public double A { get; }
    public double B { get; }
    public double C { get; }

 public Triangle(double a, double b, double c)
{
    if (a <= 0) throw new ArgumentException("Side a must be positive.", nameof(a));
    if (b <= 0) throw new ArgumentException("Side b must be positive.", nameof(b));
    if (c <= 0) throw new ArgumentException("Side c must be positive.", nameof(c));

    // Triangle inequality
    if (a + b <= c || a + c <= b || b + c <= a)
        throw new ArgumentException("Triangle inequality violated: each side must be less than the sum of the other two sides.");

    A = a; B = b; C = c;
}


    public double Perimeter() => A + B + C;

    public double Area()
    {
        double s = Perimeter() / 2;
        return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
    }
}

