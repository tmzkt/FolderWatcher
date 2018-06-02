using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

namespace FolderWatcher
{
    class Program
    {
        static string path;
        static string filter;

        static void Main(string[] args)
        {
            InitializeLogger();
            LoadConfiguration();

            try
            {
                FolderWatcher watcher = new FolderWatcher(path, filter);
                watcher.Start();
                Console.ReadLine();
                watcher.Stop();
            }
            catch (Exception e)
            {
                Log.Error(e, "Unexpected error");
            }

            Console.ReadLine();
        }

        static void InitializeLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();
        }

        static void LoadConfiguration()
        {
            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
                path = configuration["Path"];
                filter = configuration["Filter"];
            }
            catch (Exception e)
            {
                Log.Error(e, "Error loading configuration");
                throw e;
            }
        }
    }
}
