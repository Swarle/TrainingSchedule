using PLL.Data.Dao.DaoFactory;
using PLL.Data.Dao.Interfaces;
using PLL.Data.Dao.SqlDao;

namespace PLL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IDaoAccessor>(provider =>
            {
                var factory = new SqlDaoAccessFactory(provider.GetService<ILoggerFactory>());

                return factory.GetAccessor();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();


            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
