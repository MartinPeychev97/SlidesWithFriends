using BAL.Interfaces;
using BAL.Services;
using DAL;
using DAL.EntityModels.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RandomNameGeneratorLibrary;
using System;
using web_app.Hubs;

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
                    opts.ClientId = Configuration["Authentication:Google:ClientId"];
					opts.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
					opts.SignInScheme = IdentityConstants.ExternalScheme;
                    opts.ClaimActions.MapJsonKey("image", "picture");
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
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
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

            services.AddSignalR();

            services.AddHttpContextAccessor();

            services.AddTransient<ISlideService, SlideService>();
            services.AddTransient<IPresentationService, PresentationService>();

            services.AddSingleton<PlaceNameGenerator>();
            services.AddTransient<IUserService, UserService>();

            services.AddHttpClient<IUsernameGenerator, UsernameGenerator>();
            services.AddSingleton<Random>();

            services.AddTransient<IRatingService, RatingService>();

            services.AddSingleton<PresentationHub>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

			app.UseCookiePolicy(new CookiePolicyOptions {
				Secure = CookieSecurePolicy.Always
			});
			app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<PresentationHub>("/hubs/presentation");
            });
        }
    }
}
