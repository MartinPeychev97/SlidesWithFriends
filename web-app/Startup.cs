using BAL.Interfaces;
using BAL.Services;
using DAL;
using DAL.EntityModels.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RandomNameGeneratorLibrary;
using System;

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
            services.AddAuthentication()
                .AddGoogle(opts =>
                {
                    opts.ClientId = "350014667085-dj0la8tmuqvbcp2o72atcmc1deake5c8.apps.googleusercontent.com";
                    opts.ClientSecret = "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";
                    opts.SignInScheme = IdentityConstants.ExternalScheme;
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

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/Login";
            });

            services.AddMvc()
                    .AddViewLocalization(
                        LanguageViewLocationExpanderFormat.Suffix,
                        options => { options.ResourcesPath = "Resources"; })
                    .AddDataAnnotationsLocalization();


            services.AddRazorPages();

            services.AddTransient<ISlideService, SlideService>();
            services.AddTransient<IPresentationService, PresentationService>();

            services.AddSingleton<PlaceNameGenerator>();
            services.AddTransient<IUserService, UserService>();
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
