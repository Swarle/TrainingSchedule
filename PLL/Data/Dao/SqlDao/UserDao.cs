using System.Data.SqlClient;
using PLL.Data.Entity;
using System.Data;

namespace PLL.Data.Dao.SqlDao
{
    public class UserDao : SqlDaoAbstract<User>
    {
        protected override string SelectAllRequest => "Select * From [User]";
        protected override string SelectByIdRequest => "Select * From [User] Where id = @Id";
        protected override string InsertRequest => "Insert Into [User](login,password,role_id) Values (@Login,@Password,@RoleId)";
        protected override string UpdateRequest => "Update [User] login = @Login, password = @Password, role_id = @RoleId Where id = @Id";
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
                RoleId = reader.GetGuid("role_id").ToString()
            };
        }

        protected override SqlCommand ToSqlRequest(User entity, string request)
        {
            var command = new SqlCommand(request,_connection);

            command.Parameters.AddWithValue("@Login", entity.Login);
            command.Parameters.AddWithValue("@Password", entity.Password);
            command.Parameters.AddWithValue("RoleId", entity.RoleId);
            
            return command;
        }
    }
}
