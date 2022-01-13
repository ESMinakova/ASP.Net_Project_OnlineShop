using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShopWebApp.Models;
using System;
using Serilog;
using OnlineShop.Db;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;
using Microsoft.AspNetCore.Http;

namespace OnlineShopWebApp
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
            //добавление контекста DatabaseContext в качестве сервиса в приложении
            string connection = Configuration.GetConnectionString("online_shop_minakova");
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(connection));

            //добавление контектса IdentityContext в качестве сервиса в приложении
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(connection));

            services.AddIdentity<User, IdentityRole>() //тип пользователя и роль
                .AddEntityFrameworkStores<IdentityContext>(); // указываем тип хранилища данных о юзере и ролях

            //добавляем куки
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.Cookie = new CookieBuilder
                {
                    IsEssential = true
                };
            });

            services.AddTransient<IShop, DbShop>();
            services.AddTransient<ICartRepository, DbCartsRepository>();
            services.AddTransient<IOrderRepository, DbOrderRepository>();
            services.AddTransient<IComparisonRepository, DbComparisonRepository>();
            services.AddTransient<IFavouritesRepository, DbFavouritesRepository>();                       
            services.AddSingleton<ImageProcessing>();
            services.AddControllersWithViews();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US")
                };
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
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
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();            

            var localizationOptons = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptons);

            app.UseAuthentication(); //подлкючение аутентификации
            app.UseAuthorization(); //подключение авторизации

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
