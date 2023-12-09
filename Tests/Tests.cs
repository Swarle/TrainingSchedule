using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions;
using Microsoft.Extensions.Logging;
using PLL.Data.Dao.Interfaces;
using PLL.Data.Dao.MongoDbDao;
using PLL.Data.Dao.SqlDao;
using PLL.Data.Entity;
using MongoDbUserDao = PLL.Data.Dao.MongoDbDao.UserDao;
using SqlUserDao = PLL.Data.Dao.SqlDao.UserDao;

namespace Tests
{
    internal class Tests
    {
        private readonly IDaoAccessor _sqlDaoAccessor;
        private readonly IDaoAccessor _mongoDbAccessor;
        private Faker<User> _faker;
        private List<string> SqlRoleIdList { get; set; } = new List<string>
            { "4c8ff13b-dd21-4526-a841-b38e5fff566a", "947e2751-8fde-42e4-8ab5-f09adc061738" };

        private List<string> MongoDbRoleIdList { get; set; } = new List<string>
            { "6571d38b97048a7ec3d1ab5d", "6571d38997048a7ec3d1ab5c" };

        public Tests()
        {
            var loggerFactory = new LoggerFactory();

            _sqlDaoAccessor = new SqlDaoAccessor(loggerFactory);
            _mongoDbAccessor = new MongoDbAccessor(loggerFactory);
            _faker = new Faker<User>()
                .RuleSet("MongoDb", rule =>
                {
                    rule.RuleFor(u => u.Login, f => f.Internet.UserName().ClampLength(max:25));
                    rule.RuleFor(u => u.Password, f => f.Internet.Password().ClampLength(max: 25));
                    rule.RuleFor(u => u.Email, f => f.Internet.Email().ClampLength(max: 100));
                    rule.RuleFor(u => u.Age, f => f.Random.Number(17, 67));
                    rule.RuleFor(u => u.RoleId, f => f.PickRandom(MongoDbRoleIdList));
                })
                .RuleSet("Sql", rule =>
                {
                    rule.RuleFor(u => u.Login, f => f.Internet.UserName().ClampLength(max:25));
                    rule.RuleFor(u => u.Password, f => f.Internet.Password().ClampLength(max: 25));
                    rule.RuleFor(u => u.Email, f => f.Internet.Email().ClampLength(max:100));
                    rule.RuleFor(u => u.Age, f => f.Random.Number(17, 67));
                    rule.RuleFor(u => u.RoleId, f => f.PickRandom(SqlRoleIdList));
                });
        }

        public void TestMongoDbAsync(int numberOfEntity)
        {
            var userDao = _mongoDbAccessor.UserDao as MongoDbUserDao;

            var timer = new Stopwatch();

            var fakeUserList = _faker.Generate(numberOfEntity, "MongoDb");

            timer.Start();
            
            userDao.CreateManyAsync(fakeUserList);

            timer.Stop();

            Console.WriteLine($"Було додано {numberOfEntity} користувачів до БД за {timer.Elapsed}");

            timer.Restart();

            var userList = userDao.GetAll();

            timer.Stop();

            Console.WriteLine($"Було зчитано {userList.Count} сутностей з БД за {timer.Elapsed}");

            userDao.DeleteAll("User");
        }

        public void TestSqlAsync(int numberOfEntity)
        {
            var userDao = _sqlDaoAccessor.UserDao as SqlUserDao;

            try
            {
                var timer = new Stopwatch();

                var fakeUserList = _faker.Generate(numberOfEntity, "Sql");

                timer.Start();

                userDao.CreateManyAsync(fakeUserList);

                timer.Stop();

                Console.WriteLine($"Було додано {numberOfEntity} користувачів до БД за {timer.Elapsed}");

                timer.Restart();

                var userList = userDao.GetAll();

                timer.Stop();

                Console.WriteLine($"Було зчитано {userList.Count} сутностей з БД за {timer.Elapsed}");

                userDao.DeleteAll("User");
            }
            catch (Exception ex)
            {
                userDao.DeleteAll("User");
                throw;
            }
        }
    }
}
