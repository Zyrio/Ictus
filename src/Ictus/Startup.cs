using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yio.Data;
using Yio.Data.Constants;
using Yio.Data.Repositories;
using Yio.Data.Repositories.Interfaces;
using Yio.Utilities;
using Yio.Utilities.Interfaces;

namespace Yio
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            AppSettingsConstant.External_DISQUS = Configuration.GetSection("AppSettings").GetSection("External").GetSection("DISQUS").Value;
            AppSettingsConstant.External_GoogleAnalytics = Configuration.GetSection("AppSettings").GetSection("External").GetSection("GoogleAnalytics").Value;
            AppSettingsConstant.FileEndpoint = Configuration.GetSection("AppSettings").GetSection("FileEndpoint").Value;
            AppSettingsConstant.FileStorage = Configuration.GetSection("AppSettings").GetSection("FileStorage").Value;
            AppSettingsConstant.SiteIcon = Configuration.GetSection("AppSettings").GetSection("SiteIcon").Value;
            AppSettingsConstant.SiteName = Configuration.GetSection("AppSettings").GetSection("SiteName").Value;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();
            services.AddOptions();

            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<IFileTagRepository, FileTagRepository>();
            services.AddTransient<IRandomGeneratorUtilities, RandomGeneratorUtilities>();
            services.AddTransient<ITagRepository, TagRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
