# 🧮 GeometrySolution

A clean, test-driven .NET 9 application demonstrating modern software engineering practices including **dependency injection**, **structured logging with Serilog**, and **per-environment configuration**.  
This solution models a set of geometric shapes, each capable of computing its own area and perimeter through a shared `IShape` contract.

---

## 🚀 Quickstart

```bash
# Clone and build
git clone https://github.com/michael-j-morgan/GeometrySolution.git
cd GeometrySolution
dotnet build

# Run the app
dotnet run --project GeometryApp
```

---

## 📁 Solution Structure

| Project | Purpose |
|----------|----------|
| **GeometryLib** | Core library containing all shape implementations (`Circle`, `Square`, `Triangle`, etc.) and the `IShape` interface. |
| **GeometryApp** | Console application that registers shapes via dependency injection, logs output using Serilog, and supports per-environment configurations (`appsettings.Development.json`, `appsettings.Production.json`). |
| **GeometryTests** | xUnit test project validating geometry calculations using a Test-Driven Development (TDD) approach. |

---

## ⚙️ Features

- **Test-Driven Development (TDD)** — Shapes are implemented incrementally based on failing tests.
- **Dependency Injection (DI)** — Shapes are registered via `IServiceCollection` for flexible composition.
- **Structured Logging with Serilog** — Console and JSON log sinks for clean observability.
- **Per-Environment Configuration** — Supports `appsettings.{Environment}.json` and environment variable overrides.
- **Colored Console Output** — Highlights diagnostic messages for better readability.
- **Automatic Log Retention Policy** — Old log folders automatically pruned based on configurable retention days.
- **Configuration Diagnostics** — Detects and logs environment overrides in structured JSON format.

---

## 🧩 Implemented Shapes

| Shape | Formula | Notes |
|--------|----------|--------|
| **Circle** | πr² | Validates radius > 0 |
| **Square** | s² | Supports area & perimeter |
| **Triangle** | Heron’s formula | Warns when area < 10 |
| **Ellipse** | πab | Uses Ramanujan’s 2nd approximation (<0.04% error) |
| **Rectangle** | width × height | Validates positive dimensions |
| **Parallelogram** | base × height | Consistent validation |
| **Trapezoid** | ((a+b)/2) × h | Requires all sides > 0 |

---

## 🧱 Example Output

```bash
[14:09:55 INF] (Development/1) Circle → Area: 78.54, Perimeter: 31.42
[14:09:55 WRN] (Development/1) Triangle has a small area (<10): 6.00
[14:09:55 INF] (Development/1) All calculations completed successfully.
```

---

## 🧩 Key Concepts

### `IShape` Interface
Defines the contract for all geometric entities:
```csharp
public interface IShape
{
    string Name { get; }
    double Area();
    double Perimeter();
}
```

### Dependency Injection Setup
```csharp
var services = new ServiceCollection()
    .AddTransient<IShape>(sp => new Circle(5))
    .AddTransient<IShape>(sp => new Square(4))
    .AddTransient<IShape>(sp => new Triangle(3, 4, 5))
    .BuildServiceProvider();
```

### Serilog Configuration
Configured through `appsettings.json`:
```json
{
  "Serilog": {
    "MinimumLevel": "Debug",
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] ({ThreadId}) {Message:lj}{NewLine}{Exception}",
          "theme": "Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme::Code, Serilog.Sinks.Console"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/geometry-dev.json",
          "rollingInterval": "Day",
          "formatter": "Serilog.Formatting.Json.JsonFormatter, Serilog"
        }
      }
    ]
  },
  "Logging": {
    "Retention": { "Days": 14 }
  }
}
```

---

## 🧪 Running Tests

```bash
dotnet test
# or continuous test watching
dotnet watch test
```

> ✅ Includes 20+ xUnit tests covering shape calculations, edge cases, configuration diagnostics, and log retention behavior.

---

## 🧹 Log Retention

Old logs are automatically cleaned up at startup based on the configured number of retention days in your `appsettings.json`:

```json
"Logging": {
  "Retention": { "Days": 14 }
}
```

---

## 💡 Future Enhancements

- Add validation for degenerate or undefined shapes.
- Extend to 3D solids (Sphere, Cube, Cylinder) with volume/surface computations.
- Add CLI parameters for dynamic shape input.
- Introduce metrics aggregation for batch shape processing.
- Implement `IShapeFactory` and segregated interfaces (`IAreaCalculable`, `IPerimeterCalculable`).

---

## 🧑‍💻 Author

**Michael Morgan**  
Focus areas: Test Automation, CI/CD Pipelines, DevSecOps, and .NET Application Architecture.

---

## 🪪 License

This project is licensed under the [MIT License](LICENSE).

---

[![.NET Build](https://github.com/michael-j-morgan/GeometrySolution/actions/workflows/main.yml/badge.svg)](https://github.com/michael-j-morgan/GeometrySolution/actions)
