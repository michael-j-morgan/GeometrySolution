namespace GeometryLib;

public class Triangle : IShape
{
    public string Name { get; } = "Triangle";
    public double A { get; }
    public double B { get; }
    public double C { get; }

    public Triangle(double a, double b, double c)
    {
        if (a + b <= c || a + c <= b || b + c <= a)
            throw new ArgumentException("Invalid triangle sides.");

        A = a; B = b; C = c;
    }

    public double Perimeter() => A + B + C;

    public double Area()
    {
        double s = Perimeter() / 2;
        return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
    }
}

