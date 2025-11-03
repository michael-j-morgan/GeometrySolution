using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeometryApp
{
    public static class ConfigDiagnostics
    {
        public static void PrintOverrides(string environment)
        {
#if DEBUG
            using (LogContext.PushProperty("Component", "ConfigDiagnostics"))
            {
                Console.WriteLine("\n🔹 Comparing configuration values between base and environment:\n");

                // ANSI color helpers
                string Green(string text) => $"\u001b[32m{text}\u001b[0m";
                string Cyan(string text)  => $"\u001b[36m{text}\u001b[0m";

                // ✅ Base config (appsettings.json)
                var baseConfig = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                // ✅ Environment config (appsettings.json + appsettings.{env}.json)
                var envConfig = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddJsonFile($"appsettings.{environment}.json", optional: true)
                    .Build();

                // Flatten hierarchical configuration into key:value pairs
                Dictionary<string, string?> Flatten(IConfiguration config, string? parentKey = null)
                {
                    var dict = new Dictionary<string, string?>();
                    foreach (var child in config.GetChildren())
                    {
                        var key = string.IsNullOrEmpty(parentKey) ? child.Key : $"{parentKey}:{child.Key}";
                        if (child.Value != null)
                            dict[key] = child.Value;
                        else
                            foreach (var kv in Flatten(child, key))
                                dict[kv.Key] = kv.Value;
                    }
                    return dict;
                }

                var baseDict = Flatten(baseConfig);
                var envDict  = Flatten(envConfig);

                int overrides = 0, added = 0;

                foreach (var kv in baseDict)
                {
                    if (envDict.TryGetValue(kv.Key, out var envValue) && envValue != kv.Value)
                    {
                        overrides++;
                        var message = $"✅ Override → {kv.Key}: '{kv.Value}' → '{envValue}'";
                        Console.WriteLine(Green(message));
                        Log.ForContext("Key", kv.Key)
                           .ForContext("OldValue", kv.Value)
                           .ForContext("NewValue", envValue)
                           .Information("Configuration override detected.");
                    }
                }

                foreach (var kv in envDict)
                {
                    if (!baseDict.ContainsKey(kv.Key))
                    {
                        added++;
                        var message = $"🆕 Added → {kv.Key}: '{kv.Value}'";
                        Console.WriteLine(Cyan(message));
                        Log.ForContext("Key", kv.Key)
                           .ForContext("NewValue", kv.Value)
                           .Information("Configuration key added in environment config.");
                    }
                }

                Console.WriteLine();
                Log.Information("Configuration diagnostics summary: {Overrides} overrides, {Added} new keys found in {Environment} configuration.",
                    overrides, added, environment);
            }
#endif
        }
    }
}
