using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Yio.Utilities;

namespace Yio
{
    public class Program
    {
        private static int _port { get; set; }

        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            StartupUtilities.WriteStartupMessage(StartupUtilities.GetRelease(), ConsoleColor.Cyan);

            if (!File.Exists("appsettings.json")) {
                StartupUtilities.WriteFailure("configuration is missing");
                Environment.Exit(1);
            }

            StartupUtilities.WriteInfo("setting port");
            if(args == null || args.Length == 0)
            {
                _port = 5100;
                StartupUtilities.WriteSuccess("port set to " + _port + " (default)");
            }
            else
            {
                _port = args[0] == null ? 5100 : Int32.Parse(args[0]);
                StartupUtilities.WriteSuccess("port set to " + _port + " (user defined)");
            }

            StartupUtilities.WriteInfo("starting App");
            BuildWebHost().Run();

            Console.ResetColor();
        }

        public static IWebHost BuildWebHost() =>
            WebHost.CreateDefaultBuilder()
                .UseUrls("http://0.0.0.0:" + _port.ToString() + "/")
                .UseStartup<Startup>()
                .Build();
    }
}
