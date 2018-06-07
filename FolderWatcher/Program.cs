using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

namespace FolderWatcher
{
    public class Program
    {
        static string path;
        static string filter;

        static void Main(string[] args)
        {
            InitializeLogger();
            if (!LoadConfiguration())
            {
                return;
            }

            try
            {
                FolderWatcher watcher = new FolderWatcher(new FileSystemWatcherWrapper(new FileSystemWatcher(path, filter)));
                watcher.Start();
                Console.ReadLine();
                watcher.Stop();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Log.Error(e, "Unexpected error");
            }
        }

        static void InitializeLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();
        }

        static bool LoadConfiguration()
        {
            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
                path = GetConfigurationValue(configuration, "Path");
                filter = GetConfigurationValue(configuration, "Filter");
            }
            catch (Exception e)
            {
                Log.Error(e, "Error loading configuration");
                return false;
            }

            return true;
        }

        static string GetConfigurationValue(IConfiguration configuration, string key)
        {
            string value = configuration[key];
            if (value == null)
            {
                throw new Exception($"'{key}' key not found in configuration");
            }
            return value;
        }
    }
}
