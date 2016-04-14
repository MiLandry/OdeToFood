using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OdeToFood.Services;

namespace OdeToFood
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            Configuration = builder.Build();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(provider => Configuration);
            services.AddSingleton<IGreeter, Greeter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IGreeter greeter,
            IHostingEnvironment environment)
        {
            app.UseIISPlatformHandler();

            if (environment.IsDevelopment())  //Logical environment information inferred from an enviroment variable ("Hosting:Envirconment - Development")
            {
                app.UseDeveloperExceptionPage();

            }




            app.UseRuntimeInfoPage();

            //app.UseDefaultFiles();
            //app.UseStaticFiles();

            app.UseFileServer();

            app.UseMvcWithDefaultRoute();



            app.Run(async (context) =>
            {
                //throw new System.Exception("Error");


                var greeting = greeter.getGreeting();
                await context.Response.WriteAsync(greeting);
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
