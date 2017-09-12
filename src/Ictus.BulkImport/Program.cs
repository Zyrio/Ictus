using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yio.BulkImport.Data;
using Yio.BulkImport.Data.Constants;
using Yio.Data.Repositories;
using Yio.Data.Repositories.Interfaces;

namespace Yio.BulkImport
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration;
            var services = new ServiceCollection();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            ArgConstants.MoveFrom = args[0];
            ArgConstants.MoveTo = args[1];
            configuration = builder.Build();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<Business.Import>();

            var provider = services.BuildServiceProvider();

            using (var businessService = provider.GetService<Business.Import>())
            {
                
            }
        }
    }
}
