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
        var ex = Record.Exception(() => ConfigDiagnostics.PrintOverrides("Production"));
        Assert.Null(ex); // ✅ Should not throw, even if no overrides found
    }

}
