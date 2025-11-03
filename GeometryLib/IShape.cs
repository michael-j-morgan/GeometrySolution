namespace GeometryLib;
public interface IShape
{
    string Name { get; }
    double Area();
    double Perimeter();
}

