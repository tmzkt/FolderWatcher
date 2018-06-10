using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using System.Threading;

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
                WaitForControlC();
                watcher.Stop();
            }
            catch (Exception e)
            {
                Log.Error(e, "Unexpected error");
            }
        }

        static void InitializeLogger()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss.fff}] {Message:lj}{NewLine}{Exception}")
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

        static void WaitForControlC()
        {
            var exitEvent = new ManualResetEvent(false);
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                exitEvent.Set();
            };
            exitEvent.WaitOne();
        }
    }
}
