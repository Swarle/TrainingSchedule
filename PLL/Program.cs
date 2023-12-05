using Microsoft.AspNetCore.SignalR;
using PLL.Data.Dao.DaoFactory;
using PLL.Data.Dao.Interfaces;
using PLL.Data.Dao.SqlDao;
using PLL.Data.Entity;
using PLL.Data.Observer;
using PLL.Data.Observer.Interfaces;
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
            
            builder.Services.AddSingleton<IObserver,Observer>();

            builder.Services.AddSingleton<IDaoAccessor>(provider =>
            {
                var factory = new SqlDaoAccessFactory(provider.GetService<ILoggerFactory>(), provider.GetRequiredService<IObserver>());
                
                return factory.GetAccessor();
            });

            builder.Services.AddSession();
            
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseHsts();
            }

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
