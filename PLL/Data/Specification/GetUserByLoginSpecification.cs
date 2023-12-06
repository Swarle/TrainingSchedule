using System.Data.SqlClient;
using PLL.Data.Entity;
using System.Data;

namespace PLL.Data.Specification
{
    public class GetUserByLoginSpecification : ISpecification<User>
    {
        private readonly string _login;

        public GetUserByLoginSpecification(string login)
        {
            _login = login;
        }

        public SqlCommand CreateCommand(SqlConnection connection)
        {
            var request = new string("Select * From [User] Where login = @Login");
            var command = new SqlCommand(request, connection);

            command.Parameters.AddWithValue("@Login", _login);

            return command;
        }

        public User MapEntity(SqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetGuid("id").ToString(),
                Login = reader.GetString("login"),
                Password = reader.GetString("Password"),
                RoleId = reader.GetGuid("role_id").ToString()
            };
        }
    }
}
