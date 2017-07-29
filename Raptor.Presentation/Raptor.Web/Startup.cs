using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Raptor.Services.Blog;
using Raptor.Services.Configuration;
using Raptor.Services.Content;
using Raptor.Services.Logging;
using Raptor.Services.Security;
using Raptor.Services.Users;

namespace Raptor.Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env) {
            // Build the basic configuration for our web app
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services) {
            // Enable lowercases routes for urls
            services.AddRouting(options => options.LowercaseUrls = true);

            // Add MVC to our web app
            services.AddMvc();

            // Register our services
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<ISettingService, SettingService>();
            services.AddTransient<IContentService, ContentService>();
            services.AddTransient<ICustomerActivityService, CustomerActivityService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IUserRegisterationService, UserRegistrationService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole();

            // Register the routes for areas
            app.UseMvc(routes => {
                routes.MapRoute("areaRoute", "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Select the right error handling pages
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
            }

            // Allow to serve static files
            app.UseStaticFiles();

            // Register MVC routes
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
