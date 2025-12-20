using POSBasic.Models;
using POSBasic.Database;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace POSBasic.Services
{
    public class ProductoService
    {
        public DataTable Listar()
        {
            var dt = new DataTable();
            try
            {
                using var cn = OracleConnectionFactory.GetConnection();
                cn.Open();

                using var da = new OracleDataAdapter("SELECT codproducto, codigobarra, desproducto, precioventa, stock FROM PRODUCTO",cn);

                da.Fill(dt);
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Error Oracle al listar productos:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general al listar productos:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
        public bool Insertar(Productos p)
        {
            try
            {
                using var cn = OracleConnectionFactory.GetConnection();
                cn.Open();

                using var cmd = new OracleCommand("sp_insertar_producto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(":p_codigobarra", p.codigobarra);
                cmd.Parameters.Add(":p_desproducto", p.desproducto);
                cmd.Parameters.Add(":p_precioventa", p.precioventa);
                cmd.Parameters.Add(":p_stock", p.stock);

                cmd.ExecuteNonQuery();

                return true;
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Error Oracle: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool Actualizar(Productos p)
        {
            try
            {
                using var cn = OracleConnectionFactory.GetConnection();
                cn.Open();
                using var tran = cn.BeginTransaction();
                using var cmd = new OracleCommand("SP_ACTUALIZAR_PRODUCTO", cn)
                {
                    CommandType = CommandType.StoredProcedure,
                    Transaction = tran
                };
                cmd.Parameters.Add("p_codproducto", OracleDbType.Int32).Value = p.codproducto;
                cmd.Parameters.Add("p_codigobarra", OracleDbType.Varchar2).Value = p.codigobarra;
                cmd.Parameters.Add("p_desproducto", OracleDbType.Varchar2).Value = p.desproducto;
                cmd.Parameters.Add("p_precioventa", OracleDbType.Decimal).Value = p.precioventa;
                cmd.Parameters.Add("p_stock", OracleDbType.Decimal).Value = p.stock;
                cmd.ExecuteNonQuery();
                tran.Commit();
                return true;
            }
            catch (OracleException ex)
            {
                MessageBox.Show( ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar producto: " + ex.Message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Eliminar(int codproducto)
        {
            try
            {
                using var cn = OracleConnectionFactory.GetConnection();
                cn.Open();
                using var tran = cn.BeginTransaction();
                using var cmd = new OracleCommand("SP_ELIMINAR_PRODUCTO", cn)
                {
                    CommandType = CommandType.StoredProcedure,
                    Transaction = tran
                };
                cmd.Parameters.Add("p_codproducto", OracleDbType.Int32).Value = codproducto;

                cmd.ExecuteNonQuery();
                tran.Commit();

                return true;
            }
            catch (OracleException ex)
            {
                MessageBox.Show( ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show( "Error al eliminar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
