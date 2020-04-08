using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using nadis.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nadis.tools;
using Microsoft.AspNetCore.Localization;
using nadis.DAL.nadis;
using System.Globalization;

namespace nadis
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var appSettingsJson = AppSettingJSON.GetAppSettings();
            //var connection = AppSettingJSON.GetAppSettings()["DefaultConnection"];

            //var connection = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("ru-RU");
                //SupportedCultures = supportedCultures,
                //SupportedUICultures = supportedCultures                
            } );
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                });
            //            services.Configure<CookiePolicyOptions>(options =>
            //            {
            //                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //                options.CheckConsentNeeded = context => true;
            //                options.MinimumSameSitePolicy = SameSiteMode.None;
            //            });

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            //services.AddControllersWithViews
            services.AddControllersWithViews()
                    .AddDataAnnotationsLocalization()
                    .AddViewLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var supportedCultures = new[]
                {
                    new CultureInfo("ru-RU"),
                    new CultureInfo("ru"),
                };            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();

            }
            app.UseRequestLocalization( new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru-RU"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
