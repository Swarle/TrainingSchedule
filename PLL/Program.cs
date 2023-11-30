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

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IDaoAccessor>(provider =>
            {
                var factory = new SqlDaoAccessFactory(provider.GetService<ILoggerFactory>());

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


            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
