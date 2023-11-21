using System.Data;
using System.Data.SqlClient;
using PLL.Data.Builder;
using PLL.Data.Entity;

namespace PLL.Data.Dao.SqlDao
{
    public class MuscleGroupDao : SqlDaoAbstract<MuscleGroup>

    {
        private readonly MuscleGroupBuilder _builder;

        protected override string SelectAllRequest => "Select * From Muscle_group";
        protected override string SelectByIdRequest => "Select * From Muscle_group Where id = @Id";
        protected override string InsertRequest => "Insert Into Muscle_group([group_name]) Values(@GroupName)";
        protected override string UpdateRequest => "Update Muscle_group Set [group_name] = @GroupName Where id = @Id";
        protected override string DeleteRequest => "Delete From Muscle_group Where id = @Id";

        public MuscleGroupDao(ILogger logger) : base(logger)
        {
            _builder = new MuscleGroupBuilder();
        }

        protected override MuscleGroup MapDataReaderToEntity(SqlDataReader reader)
        {
            return _builder
                .AddId(reader.GetGuid("id").ToString())
                .AddGroupName(reader.GetString("group_name"))
                .Build();
        }

        protected override SqlCommand ToSqlRequest(MuscleGroup entity)
        {
            var command = new SqlCommand(InsertRequest, _connection);

            command.Parameters.AddWithValue("@GroupName", entity.GroupName);

            return command;
        }

    }
}
