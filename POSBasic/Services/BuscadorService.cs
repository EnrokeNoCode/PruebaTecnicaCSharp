using Oracle.ManagedDataAccess.Client;
using POSBasic.Database;
using System;
using System.Data;


namespace POSBasic.Services
{
    public class BuscadorService
    {
        public DataTable Listar(string nombreTabla, string campoCodigo, string campoDescripcion, string campoExtra1 = "", string campoExtra2 = "", string campoExtra3 = "", string whereCond = "")
        {
            var dt = new DataTable();
            try
            {
                using var cn = OracleConnectionFactory.GetConnection();
                cn.Open();

                string sql = $"SELECT {campoCodigo} AS Codigo, {campoDescripcion} AS Descripcion";

                if (!string.IsNullOrEmpty(campoExtra1))
                    sql += $", {campoExtra1} as CampoExtra1";

                if (!string.IsNullOrEmpty(campoExtra2))
                    sql += $", {campoExtra2} as CampoExtra2";

                if (!string.IsNullOrEmpty(campoExtra3))
                    sql += $", {campoExtra3} as CampoExtra3";

                sql += $" FROM {nombreTabla}";
                if (!string.IsNullOrEmpty(whereCond))
                    sql += $" WHERE {whereCond}";

                using var da = new OracleDataAdapter(sql, cn);
                da.Fill(dt);
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Error Oracle al listar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general al listar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }
    }
}
