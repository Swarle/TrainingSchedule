using PLL.Data.Entity;
using System.Data;
using System.Data.SqlClient;

namespace PLL.Data.Specification
{
    public class GetRoleByRoleNameSpecification : ISpecification<Role>
    {
        private readonly string _roleName;

        public GetRoleByRoleNameSpecification(string roleName)
        {
            _roleName = roleName;
        }

        public SqlCommand CreateCommand(SqlConnection connection)
        {
            var request = new string("Select * From [Role] Where role_name = @RoleName");
            var command = new SqlCommand(request, connection);

            command.Parameters.AddWithValue("@RoleName", _roleName);

            return command;
        }

        public Role MapEntity(SqlDataReader reader)
        {
            return new Role
            {
                Id = reader.GetGuid("id").ToString(),
                RoleName = reader.GetString("role_name"),
            };
        }
    }
}
