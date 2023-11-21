using System.Data;
using System.Data.SqlClient;
using PLL.Data.Builder;
using PLL.Data.Entity;

namespace PLL.Data.Dao.SqlDao
{
    public class SetDao : SqlDaoAbstract<Set>
    {
        private readonly SetBuilder _builder;
        protected override string SelectAllRequest => "Select * From [Set]";
        protected override string SelectByIdRequest => "Select * From [Set] Where id = @Id";

        protected override string InsertRequest =>
            "Insert Into [Set]([number_repetition],[weight],[exercise_id],[unit_id]) " +
            "Value(@NumberRepetition,@Weight,@ExerciseId,@UnitId)";

        protected override string UpdateRequest =>
            "Update [Set] Set [number_repetition] = @NumberRepetition,[weight] = @Weight, [exercise_id] = @ExerciseId,[unit_id] = @UnitId " +
            "Where id = @Id";

        protected override string DeleteRequest => "Delete From [Set] Where id = @Id";

        public SetDao(ILogger logger) : base(logger)
        {
            _builder = new SetBuilder();
        }

        protected override Set MapDataReaderToEntity(SqlDataReader reader)
        {
            return _builder
                .AddId(reader.GetGuid("id").ToString())
                .AddNumberRepetition(reader.GetInt32("number_repetition"))
                .AddWeight(reader.GetInt32("weight"))
                .AddUnitId(reader.GetGuid("unit_id").ToString())
                .AddExerciseId(reader.GetGuid("exercise_id").ToString())
                .Build();
        }

        protected override SqlCommand ToSqlRequest(Set entity, string request)
        {
            var command = new SqlCommand(request, _connection);

            command.Parameters.AddWithValue("@NumberRepetition", entity.NumberRepetitons);
            command.Parameters.AddWithValue("@Weight", entity.Weight);
            command.Parameters.AddWithValue("@ExerciseId", entity.ExerciseId);
            command.Parameters.AddWithValue("@UnitId", entity.UnitId);

            return command;
        }
    }
}
