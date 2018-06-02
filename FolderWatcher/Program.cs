using System;
using System.IO;
//using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace FolderWatcher
{
    class Program
    {
        private string path;
        private string filterstring;

        static void Main(string[] args)
        {
            //ILoggerFactory logger = new LoggerFactory();
            //ILogger l = logger.CreateLogger("asdf");
            //l.LogInformation("hello");

            Log.Logger = new LoggerConfiguration().WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day).CreateLogger();
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
            catch(Exception ex)
            {
                // TODO log
                Console.WriteLine("Unexpected exception occurred. See error log for details.");
                Console.ReadLine();
            }
        }
        

        private void ParseArguments(string[] args)
        {
            if (args.Length == 0 || args.Length > 2)
            {
                Console.WriteLine("Requires path argument");
            }

            if (args.Length >= 1)
            {
                path = args[0];
            }
            if (args.Length >= 2)
            {
                filterstring = args[1];
            }
        }
    }
}
