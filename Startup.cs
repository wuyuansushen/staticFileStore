using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace staticFileTutorial
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();//Add directory Browser middleware
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            

            //Enable 'wwwroot' Directory.Alought "wwwroot" Directory can't Brose, but you could use resource files in it directly with path
            app.UseStaticFiles();


            string locationPath = env.ContentRootPath + @"/store";
            var fileLocation = new PhysicalFileProvider(locationPath);
            app.UseStaticFiles(new StaticFileOptions() { RequestPath = "/ftp", FileProvider=fileLocation });
            app.UseDirectoryBrowser(options:(new DirectoryBrowserOptions() { RequestPath="/ftp",FileProvider=fileLocation}));
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"This is a simulation of FTP!");
                });
            });
        }
    }
}
