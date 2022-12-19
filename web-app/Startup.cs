using DAL;
using DAL.EntityModels.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace web_app
{
    public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
        }

		public IConfiguration Configuration { get; }

		
		public void ConfigureServices(IServiceCollection services)
		{
            services.AddAuthentication(option =>
            {
                option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            })
                .AddCookie(option =>
                {
                    option.LoginPath = "/User/Login";
                })
                .AddGoogle(option =>
                {
                    option.ClientId = "794446576679-m1hvn02bpilrfbe7n9uf0sfvcrj4f61p.apps.googleusercontent.com";
                    option.ClientSecret = "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";
                });
           
            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Add("/Pages/{1}/{0}" + RazorViewEngine.ViewExtension);
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            string connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

            services.AddDbContext<SlidesDbContext>(options =>
                options.UseMySql(connectionString, serverVersion)
                       .EnableDetailedErrors());

            services.AddDefaultIdentity<SlidesUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<SlidesDbContext>();

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/User/Login";
            //});

            services.AddMvc()
                    .AddViewLocalization(
                        LanguageViewLocationExpanderFormat.Suffix,
                        options => { options.ResourcesPath = "Resources"; })
                    .AddDataAnnotationsLocalization();

            
            services.AddRazorPages();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
            app.UseDeveloperExceptionPage();

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

            app.UseEndpoints(endpoints =>
			{
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "Pages/{controller=Home}/{action=Index}/{id?}");
            });
        }
	}
}
