using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.SignalR;
using PLL.Data.Dao.DaoFactory;
using PLL.Data.Dao.Interfaces;
using PLL.Data.Dao.SqlDao;
using PLL.Data.Entity;
using PLL.Data.Observer;
using PLL.Data.Observer.Interfaces;
using PLL.Infostracture;
using PLL.Proxy;
using PLL.Services;
using PLL.Services.Interfaces;
using PLL.SignalR;

namespace PLL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddSignalR();

            builder.Services.AddSession();

            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.LoginPath = "/auth/login");
            builder.Services.AddAuthentication();

            builder.Services.AddSingleton<IObserver,Observer>();

            builder.Services.AddSingleton<IDaoAccessor>(provider =>
            {
                var factory = new MongoDbDaoAccesFactory(provider.GetService<ILoggerFactory>());
                
                return factory.GetAccessor();
            });
            
            builder.Services.AddScoped<ITrainingService, TrainingProxyService>();
            builder.Services.AddScoped<IAuthService,AuthService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSession();

            app.MapHub<DaoStateNotification>("/training/overview-training");


            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
