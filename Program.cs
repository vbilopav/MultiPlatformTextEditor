using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MultiPlatformTextEditorCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify file name you want to edit.");
                Console.WriteLine("Note: if file doesn't exists it will be created.");
                return;
            }

            var fileName = args[0];
            if (!File.Exists(fileName))
            {
                using (File.Create(fileName)) {}
            }
            FileController.Init(fileName);
            
            //FileController.Init("README.md");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
