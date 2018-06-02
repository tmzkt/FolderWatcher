using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

namespace FolderWatcher
{
    class Program
    {
        private string path;
        private string filterstring;

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();
            Log.Information("hello, can you hear me?");

            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                string path = configuration["Path"];
                string filterString = configuration["FilterString"];

                //string path = System.Configuration.ConfigurationManager.AppSettings[""];
                FolderWatcher watcher = new FolderWatcher(path, filterString);
                watcher.Start();
                Console.ReadLine();
                watcher.Stop();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                // TODO log
                Console.WriteLine("Unexpected exception occurred. See error log for details.");
                Console.ReadLine();
            }
        }
    }
}
