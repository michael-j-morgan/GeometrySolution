using Xunit;
using System;
using System.IO;
using GeometryApp;  // for CleanOldLogs()

namespace GeometryTests;

public class LogCleanupTests
{
    [Fact]
    public void CleanOldLogs_RemovesOldFolders()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), "GeometryLogsTest");
        Directory.CreateDirectory(tempDir);

        var oldFolder = Path.Combine(tempDir, DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"));
        var recentFolder = Path.Combine(tempDir, DateTime.Now.ToString("yyyy-MM-dd"));

        Directory.CreateDirectory(oldFolder);
        Directory.CreateDirectory(recentFolder);

        // Act
        typeof(Program)
            .GetMethod("CleanOldLogs", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
            .Invoke(null, new object[] { tempDir, 14 });

        // Assert
        Assert.False(Directory.Exists(oldFolder));
        Assert.True(Directory.Exists(recentFolder));

        // Cleanup
        Directory.Delete(tempDir, recursive: true);
    }
}
