namespace GeometryLib;

public class Parallelogram : IShape
{
    public string Name { get; } = "Parallelogram";
    public double BaseLength { get; }
    public double SideLength { get; }
    public double Height { get; }

    public Parallelogram(double baseLength, double sideLength, double height)
{
    if (baseLength <= 0)
        throw new ArgumentException("Base length must be positive.", nameof(baseLength));
    if (sideLength <= 0)
        throw new ArgumentException("Side length must be positive.", nameof(sideLength));
    if (height <= 0)
        throw new ArgumentException("Height must be positive.", nameof(height));

    BaseLength = baseLength;
    SideLength = sideLength;
    Height = height;
}


    public double Area() => BaseLength * Height;
    public double Perimeter() => 2 * (BaseLength + SideLength);

}
