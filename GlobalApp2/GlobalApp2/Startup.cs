using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using GlobalApp2.Localization;
using GlobalApp2.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace GlobalApp2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IHelpService, HelpService>();
            services.AddScoped<IHomeService, HomeService>();

            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.AddTransient(typeof(IStringLocalizer<>),typeof(StringLocalizer<>));

            services.Configure<JsonLocalizationOptions>(options => {
                options.ResourcesPath = "JsonResources";
            });

            services.AddMvc().AddViewLocalization(
                    opts => { opts.ResourcesPath = "JsonResources"; })
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                    .AddDataAnnotationsLocalization()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddLocalization(options =>
            //{
            //    options.ResourcesPath = "Resources";
            //});


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=About}/{id?}");
            });

            //app.Run(async (context) =>
            //{
            //    if(context.Request.Query.ContainsKey("ui-culture"))
            //    {
            //        string tag = context.Request.Query["ui-culture"];
            //        CultureInfo.CurrentUICulture = new CultureInfo(tag);
            //    }
            //    if(context.Request.Query.ContainsKey("about"))//check the query string for current request contains the key about. 
            //    {                                             //if contains extract the search term
            //        string searchTerm = context.Request.Query["about"];
            //        IAboutService service = context.RequestServices.GetService<IAboutService>();// create new instance IAboutService
            //        string content = service.Reply(searchTerm);
            //        await context.Response.WriteAsync(content);//return value from resource file.
            //        return;
            //    }
            //    if (context.Request.Query.ContainsKey("department"))//check the query string for current request contains the key about. 
            //    {                                             //if contains extract the search term
            //        string department = context.Request.Query["department"];
            //        IDepartmentService service = context.RequestServices.GetService<IDepartmentService>();// create new instance IAboutService
            //        string info = service.GetInfo(department);
            //        await context.Response.WriteAsync(info);//return value from resource file.
            //        return;
            //    }
            //    if (context.Request.Query.ContainsKey("help"))//check the query string for current request contains the key about. 
            //    {                                             //if contains extract the search term
            //        string serviceName = context.Request.Query["help"];
            //        IHelpService service = context.RequestServices.GetService<IHelpService>();// create new instance IAboutService
            //        string content = service.GetHelpFor(serviceName);
            //        await context.Response.WriteAsync(content);//return value from resource file.
            //        return;
            //    }
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
