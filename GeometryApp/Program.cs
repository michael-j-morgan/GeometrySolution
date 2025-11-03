using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Serilog;
using GeometryLib;
using GeometryApp;


class Program
{
    static void Main()
    {
        // 1️⃣ Detect environment
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
        var logRoot = Path.Combine("logs", environment);
        CleanOldLogs(logRoot, 14);
        // ✅ Tell .NET exactly where to look for appsettings.json
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory) // ✅ ensures configs are loaded from the output directory
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);


        var configuration = configurationBuilder.Build();

        // 3️⃣ Configure Serilog with enrichers and filtered console
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .WriteTo.File(
                path: Path.Combine("logs", environment, DateTime.Now.ToString("yyyy-MM-dd"), "geometry.log"),
                rollingInterval: RollingInterval.Day,
                formatter: new Serilog.Formatting.Json.JsonFormatter(),
                shared: true)
            .CreateLogger();


        // 🧠 Diagnostic logging: show where configuration files are being loaded from
#if DEBUG
        ConfigDiagnostics.PrintOverrides(environment);
#endif



        // 4️⃣ Log environment and loaded configuration files
        Log.Information("Running GeometryApp in {Environment} environment", environment);

        foreach (var source in configurationBuilder.Sources.OfType<Microsoft.Extensions.Configuration.Json.JsonConfigurationSource>())
        {
            Log.Information("Loaded configuration file: {File}", source.Path);
        }

        // ✅ Check for the file in the output (base) path
        var envConfigPath = Path.Combine(AppContext.BaseDirectory, $"appsettings.{environment}.json");

        if (!File.Exists(envConfigPath) && environment != "Production")
        {
            Log.Warning("Environment configuration file not found: {File}", envConfigPath);
        }

        try
        {
            // 5️⃣ Configure dependency injection
            var services = new ServiceCollection()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddSerilog();
                })
                .AddTransient<IShape>(sp => new Circle(5))
                .AddTransient<IShape>(sp => new Square(4))
                .AddTransient<IShape>(sp => new Triangle(3, 4, 5))
                .AddTransient<IShape>(sp => new Ellipse(5, 3))
                .AddTransient<IShape>(sp => new Rectangle(4, 6))
                .AddTransient<IShape>(sp => new Parallelogram(6, 4, 5))
                .AddTransient<IShape>(sp => new Trapezoid(8, 4, 3, 5, 5))
                .BuildServiceProvider();

            // 6️⃣ Get logger and shapes
            var logger = services.GetRequiredService<ILogger<Program>>();
            var shapes = services.GetServices<IShape>();

            logger.LogInformation("Calculating areas and perimeters for {ShapeCount} shapes", shapes.Count());

            // 7️⃣ Iterate and log shape details
            foreach (var s in shapes)
            {
                logger.LogInformation("{Shape} → Area: {Area:F2}, Perimeter: {Perimeter:F2}",
                    s.Name, s.Area(), s.Perimeter());

                logger.LogDebug("Calculated raw metrics for {Shape}: Area={Area}, Perimeter={Perimeter}",
                    s.Name, s.Area(), s.Perimeter());

                if (s.Area() < 10)
                    logger.LogWarning("{Shape} has a small area (<10): {Area:F2}", s.Name, s.Area());

                if (double.IsNaN(s.Area()))
                    logger.LogError("Calculation failed for {Shape} — Area returned NaN", s.Name);
            }

            logger.LogInformation("All calculations completed successfully.");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "An unhandled exception occurred.");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
    static void CleanOldLogs(string baseLogPath, int retentionDays)
    {
        if (!Directory.Exists(baseLogPath))
            return;

        var cutoff = DateTime.Now.AddDays(-retentionDays);
        Log.Information("🧹 Checking for old log folders in {BasePath} (retention: {Days} days)", baseLogPath, retentionDays);

        foreach (var dir in Directory.GetDirectories(baseLogPath))
        {
            var folderName = Path.GetFileName(dir);

            // Expect folder name like "2025-11-03"
            if (DateTime.TryParse(folderName, out var folderDate))
            {
                if (folderDate < cutoff)
                {
                    try
                    {
                        Directory.Delete(dir, recursive: true);
                        Log.Information("🧹 Deleted old log folder {Folder} (dated {Date})", dir, folderDate.ToShortDateString());
                    }
                    catch (Exception ex)
                    {
                        Log.Warning(ex, "⚠️ Could not delete old log folder {Folder}", dir);
                    }
                }
                else
                {
                    Log.Debug("Keeping log folder {Folder} (date {Date} within retention window)", folderName, folderDate.ToShortDateString());
                }
            }
            else
            {
                Log.Debug("Skipping non-date folder {Folder}", folderName);
            }
        }

        Log.Information("✅ Log cleanup completed — retention enforced for {Days} days", retentionDays);
    }


}
