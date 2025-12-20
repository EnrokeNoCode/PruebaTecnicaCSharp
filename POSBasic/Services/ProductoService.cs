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

                using var da = new OracleDataAdapter(
                    "SELECT codproducto, codigobarra, desproducto, precioventa, stock FROM PRODUCTO",
                    cn);

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

                using var transaction = cn.BeginTransaction();
                using (var cmdVal = new OracleCommand(
                    "SELECT COUNT(*) FROM PRODUCTO WHERE CODIGOBARRA = :codigobarra AND CODPRODUCTO <> :codproducto", cn))
                {
                    cmdVal.Parameters.Add(":codigobarra", p.codigobarra);
                    cmdVal.Parameters.Add(":codproducto", p.codproducto);
                    int count = Convert.ToInt32(cmdVal.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Ya existe otro producto con ese código de barra.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                using var cmd = new OracleCommand(
                    @"UPDATE PRODUCTO SET
                        desproducto = :desproducto,
                        precioventa = :precioventa,
                        stock = :stock
                     WHERE codproducto = :codproducto", cn);

                cmd.Parameters.Add(":desproducto", p.desproducto);
                cmd.Parameters.Add(":precioventa", p.precioventa);
                cmd.Parameters.Add(":stock", p.stock);
                cmd.Parameters.Add(":codproducto", p.codproducto);

                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Eliminar(int codproducto)
        {
            if (codproducto <= 0)
            {
                MessageBox.Show("Seleccione un producto válido para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using var cn = OracleConnectionFactory.GetConnection();
                cn.Open();

                using var transaction = cn.BeginTransaction();

                using var cmd = new OracleCommand(
                    "DELETE FROM PRODUCTO WHERE CODPRODUCTO = :codproducto", cn);
                cmd.Parameters.Add(":codproducto", codproducto);
                cmd.Transaction = transaction;

                int filasAfectadas = cmd.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    MessageBox.Show("No se encontró el producto a eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    transaction.Rollback();
                    return false;
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


    }
}
