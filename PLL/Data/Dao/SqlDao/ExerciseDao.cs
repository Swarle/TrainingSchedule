using System.Data;
using System.Data.SqlClient;
using PLL.Data.Builder;
using PLL.Data.Entity;

namespace PLL.Data.Dao.SqlDao
{
    public class ExerciseDao : SqlDaoAbstract<Exercise>
    {
        private readonly ExerciseBuilder _builder;
        protected override string SelectAllRequest => "Select * From Exercise";
        protected override string SelectByIdRequest => "Select * From Exercise Where id = @Id";
        protected override string InsertRequest => "Insert Into Exercise([exercise_name],[muscle_group_id]) Values(@ExerciseName,@MuscleGroupId)";

        protected override string UpdateRequest =>
            "Update Exercise Set [exercise_name] = @ExerciseName,muscle_group_id = @MuscleGroupId Where id = @Id";

        protected override string DeleteRequest => "Delete From Exercise Where id = @Id";

        public ExerciseDao(ILogger logger) : base(logger)
        {
            _builder = new ExerciseBuilder();
        }

        protected override Exercise MapDataReaderToEntity(SqlDataReader reader)
        {
            //var entity = new Exercise
            //{
            //    Id = reader.GetGuid("id").ToString(),
            //    ExerciseName = reader.GetString("exercise_name"),
            //    MuscleGroupId = reader.GetGuid("muscle_group_id").ToString()
            //};

            return _builder
                .AddId(reader.GetGuid("id").ToString())
                .AddExerciseName(reader.GetString("exercise_name"))
                .AddMuscleGroupId(reader.GetString("muscle_group_id"))
                .Build();
        }

        protected override SqlCommand ToSqlRequest(Exercise entity,string request)
        {
            var command = new SqlCommand(request, _connection);

            command.Parameters.AddWithValue("@ExerciseName", entity.ExerciseName);
            command.Parameters.AddWithValue("@MuscleGroupId", entity.MuscleGroupId);

            return command;
        }

    }
}
