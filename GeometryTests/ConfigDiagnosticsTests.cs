using Xunit;
using GeometryApp;
using System;
using System.IO;

namespace GeometryTests;

public class ConfigDiagnosticsTests
{
    [Fact]
    public void ConfigDiagnostics_PrintsOverridesWithoutError()
    {
        // Arrange
        var env = "Development";
        var tempDir = Path.Combine(Path.GetTempPath(), "ConfigDiagTest");
        Directory.CreateDirectory(tempDir);

        File.WriteAllText(Path.Combine(tempDir, "appsettings.json"), @"{
            ""Serilog"": { ""MinimumLevel"": { ""Default"": ""Information"" } }
        }");

        File.WriteAllText(Path.Combine(tempDir, "appsettings.Development.json"), @"{
            ""Serilog"": { ""MinimumLevel"": { ""Default"": ""Debug"" } }
        }");

        var originalBaseDir = AppContext.BaseDirectory;
        AppDomain.CurrentDomain.SetData("APP_CONTEXT_BASE_DIRECTORY", tempDir);

        // Redirect console output
        using var sw = new StringWriter();
        Console.SetOut(sw);

        // Act
        ConfigDiagnostics.PrintOverrides("Development");

        // Assert
        var output = sw.ToString();
        Assert.Contains("Override", output);

        // Cleanup
        Directory.Delete(tempDir, recursive: true);
    }
    [Fact]
    public void ConfigDiagnostics_PrintsOverridesWithoutError()
    {
        var ex = Record.Exception(() => ConfigDiagnostics.PrintOverrides("Production"));
        Assert.Null(ex);
    }
}
