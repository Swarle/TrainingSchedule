using System.Data;
using System.Data.SqlClient;
using PLL.Data.Builder;
using PLL.Data.Entity;

namespace PLL.Data.Dao.SqlDao
{
    public class TrainingDao : SqlDaoAbstract<Training>
    {
        private readonly TrainingBuilder _builder;
        protected override string SelectAllRequest => "Select * From Training";
        protected override string SelectByIdRequest => "Select * From Training Where id = @Id";
        protected override string InsertRequest => "Insert into Training([date]) Values(@Date)";
        protected override string UpdateRequest => "Update Training Set date = @date Where id = @Id";
        protected override string DeleteRequest => "Delete From Training Where id = @Id";

        public TrainingDao(ILogger logger) : base(logger)
        {
            _builder = new TrainingBuilder();
        }
        
        protected override Training MapDataReaderToEntity(SqlDataReader reader)
        {
            return _builder
                .AddId(reader.GetGuid("id").ToString())
                .AddDate(reader.GetDateTime("date"))
                .Build();
        }

        protected override SqlCommand ToSqlRequest(Training entity,string request)
        {
            var command = new SqlCommand(request, _connection);

            command.Parameters.AddWithValue("@Date", entity.Date);

            return command;
        }
    }
}
