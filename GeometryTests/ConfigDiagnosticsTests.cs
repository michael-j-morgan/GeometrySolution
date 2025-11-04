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
    var sw = new StringWriter();
    Console.SetOut(sw);

    ConfigDiagnostics.PrintOverrides("Development");

    Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
    var output = sw.ToString();

    Assert.Contains("Override", output);
}
    
}
