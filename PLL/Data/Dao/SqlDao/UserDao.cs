using System.Data.SqlClient;
using PLL.Data.Entity;
using System.Data;

namespace PLL.Data.Dao.SqlDao
{
    public class UserDao : SqlDaoAbstract<User>
    {
        protected override string SelectAllRequest => "Select * From [User]";
        protected override string SelectByIdRequest => "Select * From [User] Where id = @Id";

        protected override string InsertRequest =>
            "Insert Into [User](login,password,role_id,age,email) Values (@Login,@Password,@RoleId,@Age,@Email)";

        protected override string UpdateRequest =>
            "Update [User] login = @Login, password = @Password, role_id = @RoleId,age = @Age,email = @Email Where id = @Id";

        protected override string DeleteRequest => "Delete From [User] Where id = @Id";

        public UserDao(ILogger logger) : base(logger)
        {
        }

        protected override User MapDataReaderToEntity(SqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetGuid("id").ToString(),
                Login = reader.GetString("login"),
                Password = reader.GetString("password"),
                Age = reader.GetInt32("age"),
                Email = reader.GetString("email"),
                RoleId = reader.GetGuid("role_id").ToString()

            };
        }

        protected override SqlCommand ToSqlRequest(User entity, string request)
        {
            var command = new SqlCommand(request, _connection);

            command.Parameters.AddWithValue("@Login", entity.Login);
            command.Parameters.AddWithValue("@Password", entity.Password);
            command.Parameters.AddWithValue("@RoleId", entity.RoleId);
            command.Parameters.AddWithValue("@Age", entity.Age);
            command.Parameters.AddWithValue("@Email", entity.Email);

            return command;
        }

        public void CreateManyAsync(List<User> users)
        {
            using (SqlTransaction transaction = _connection.BeginTransaction())
            {
                try
                {
                    DataTable userDataTable = new DataTable();
                    userDataTable.Columns.Add("id", typeof(Guid));
                    userDataTable.Columns.Add("login", typeof(string));
                    userDataTable.Columns.Add("password", typeof(string));
                    userDataTable.Columns.Add("role_id", typeof(Guid));
                    userDataTable.Columns.Add("age", typeof(int));
                    userDataTable.Columns.Add("email", typeof(string));


                    foreach (var user in users)
                    {
                        DataRow row = userDataTable.NewRow();
                        row["id"] = Guid.NewGuid();
                        row["login"] = user.Login;
                        row["password"] = user.Password;
                        row["age"] = user.Age;
                        row["email"] = user.Email;
                        row["role_id"] = new Guid(user.RoleId);

                        userDataTable.Rows.Add(row);
                    }

                    using (var bulkCopy = new SqlBulkCopy(_connection,SqlBulkCopyOptions.Default, transaction))
                    {
                        bulkCopy.DestinationTableName = "[TrainingSchedule].[dbo].[User]";

                        bulkCopy.WriteToServer(userDataTable);
                    }
                    
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Помилка вставки даних: {ex.Message}");
                }
            }
        }

    }
}
