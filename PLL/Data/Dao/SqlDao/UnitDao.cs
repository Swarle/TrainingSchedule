using System.Data;
using System.Data.SqlClient;
using PLL.Data.Builder;
using PLL.Data.Entity;

namespace PLL.Data.Dao.SqlDao
{
    public class UnitDao : SqlDaoAbstract<Unit>

    {
        private readonly UnitBuilder _builder;
        protected override string SelectAllRequest => "Select * From [Unit]";
        protected override string SelectByIdRequest => "Select * From [Unit] Where id = @Id";
        protected override string InsertRequest => "Insert Into [Unit]([unit_name]) Values(@UnitName)";
        protected override string UpdateRequest => "Update [Unit] Set [unit_name] = @UnitName Where id = @Id";
        protected override string DeleteRequest => "Delete From [Unit] Where id = @Id";

        public UnitDao(ILogger logger) : base(logger)
        {
            _builder = new UnitBuilder();
        }
        protected override Unit MapDataReaderToEntity(SqlDataReader reader)
        {
            return _builder
                .AddId(reader.GetGuid("id").ToString())
                .AddUnitName(reader.GetString("unit_name"))
                .Build();
        }

        protected override SqlCommand ToSqlRequest(Unit entity, string request)
        {
            var command = new SqlCommand(request, _connection);

            command.Parameters.AddWithValue("@UnitName", entity.UnitName);

            return command;
        }

    }
}
