using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Note.Business.Abstract;
using Note.Business.Concrete;
using Note.DataAccess.Abstract;
using Note.DataAccess.Concrete;
using Note.UI.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Note.UI
{
    public class Startup
    {
        private string _connectionString;

        public Startup(IConfiguration configuration)
        {
           Configuration = configuration;
            _connectionString = configuration.GetConnectionString("Identity");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(_connectionString));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSingleton<INoteService, NoteManager>();
            services.AddSingleton<INoteDal, NoteDal>();
            services.AddSingleton<ICategoryDal, CategoryDal>();
            services.AddSingleton<ICategoryService, CategoryManager>();
            services.AddDirectoryBrowser();

            services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

            });


            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(25);
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".NoteCard-Cookie",
                    SameSite = SameSiteMode.Strict
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Note}/{action=Index}/{id?}");
            });
        }
    }
}
