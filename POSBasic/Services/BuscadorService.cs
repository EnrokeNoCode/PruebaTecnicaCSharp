using Oracle.ManagedDataAccess.Client;
using POSBasic.Persistence.Interface;
using POSBasic.Services.Interfaces;
using System.Data;

namespace POSBasic.Services
{
    public class BuscadorService : IBuscadorService
    {
        private readonly IConnectionFactory _connectionFactory;

        public BuscadorService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public DataTable Listar(
            string nombreTabla,
            string campoCodigo,
            string campoDescripcion,
            string campoExtra1 = "",
            string campoExtra2 = "",
            string campoExtra3 = "",
            string whereCond = "")
        {
            try
            {
                using var cn = _connectionFactory.GetConnection();
                cn.Open();

                string sql = $"SELECT {campoCodigo} AS Codigo, {campoDescripcion} AS Descripcion";

                if (!string.IsNullOrEmpty(campoExtra1)) sql += $", {campoExtra1} AS CampoExtra1";
                if (!string.IsNullOrEmpty(campoExtra2)) sql += $", {campoExtra2} AS CampoExtra2";
                if (!string.IsNullOrEmpty(campoExtra3)) sql += $", {campoExtra3} AS CampoExtra3";

                sql += $" FROM {nombreTabla}";
                if (!string.IsNullOrEmpty(whereCond)) sql += $" WHERE {whereCond}";

                var dt = new DataTable();
                using var da = new OracleDataAdapter(sql, cn);
                da.Fill(dt);

                return dt;
            }
            catch (OracleException ex)
            {
                throw new Exception("Error Oracle al realizar la búsqueda.", ex);
            }
        }
    }
}
