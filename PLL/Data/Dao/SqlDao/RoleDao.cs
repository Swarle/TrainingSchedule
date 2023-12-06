using System.Data.SqlClient;
using PLL.Data.Entity;
using System.Data;

namespace PLL.Data.Dao.SqlDao
{
    public class RoleDao : SqlDaoAbstract<Role>
    {
        protected override string SelectAllRequest => "Select * From [Role]";
        protected override string SelectByIdRequest => "Select * From [Role] Where id = @Id";
        protected override string InsertRequest => "Insert Into [Role](role_name) Values(@RoleName)";
        protected override string UpdateRequest => "Update [Role] role_name = @RoleName Where id = @Id";
        protected override string DeleteRequest => "Delete From [Role] Where id = @Id";

        public RoleDao(ILogger logger) : base(logger)
        {
        }

        protected override Role MapDataReaderToEntity(SqlDataReader reader)
        {
            return new Role
            {
                Id = reader.GetGuid("id").ToString(),
                RoleName = reader.GetString("role_name")
            };
        }

        protected override SqlCommand ToSqlRequest(Role entity, string request)
        {
            var command = new SqlCommand(request,_connection);

            command.Parameters.AddWithValue("@RoleName", entity.RoleName);

            return command;
        }
    }
}
