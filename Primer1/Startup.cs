using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Primer1.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Primer1.Models;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Primer1
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AutomobilContext>(opcije =>
            opcije.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.Configure<IdentityOptions>(opcije=>{
                opcije.Password.RequireDigit = false;
                opcije.Password.RequiredLength = 3;
                opcije.Password.RequiredUniqueChars = 1;
                opcije.Password.RequireNonAlphanumeric = false;
                opcije.Password.RequireUppercase = false;
                opcije.SignIn.RequireConfirmedEmail = false;
                opcije.User.RequireUniqueEmail = true;
            });

            services.AddLocalization(opts => opts.ResourcesPath = "Resources");


            services.AddMvc().AddDataAnnotationsLocalization(opts =>
            {
                opts.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(Resource));
            }).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);


            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var supported = new[]
                    {
                        new CultureInfo("en"),
                        new CultureInfo("sr"),
                        new CultureInfo("de-DE")
                    };
                    opts.DefaultRequestCulture = new RequestCulture(culture:"en", uiCulture:"en-US");
                    opts.SupportedCultures = supported;
                    opts.SupportedUICultures = supported;


                }
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRequestLocalization();

            app.UseRouting();

            CultureInfo ci = new CultureInfo("sr-Latn-RS");
            ci.DateTimeFormat.DateSeparator = ".";
            ci.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            ci.NumberFormat.NumberDecimalSeparator = ".";
            List<CultureInfo> podrzaneKulture = new List<CultureInfo> { ci };
            RequestLocalizationOptions opcije = new RequestLocalizationOptions
            {
                SupportedCultures = podrzaneKulture,
                DefaultRequestCulture = new RequestCulture(ci)
            };
            app.UseRequestLocalization(opcije);



            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Automobil}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
