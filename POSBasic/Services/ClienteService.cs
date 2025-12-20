
using Oracle.ManagedDataAccess.Client;
using POSBasic.Database;
using POSBasic.Models;
using System.Data;

namespace POSBasic.Services
{
    public class ClienteService
    {
        public DataTable Listar()
        {
            var dt = new DataTable();
            try
            {
                using var cn = OracleConnectionFactory.GetConnection();
                cn.Open();

                using var da = new OracleDataAdapter(
                    "SELECT codcliente, nrodoc, nombre, apellido FROM cliente",
                    cn);

                da.Fill(dt);
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Error Oracle al listar clientes:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general al listar clientes:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        public bool Insertar(Clientes c)
        {
            try
            {
                using var cn = OracleConnectionFactory.GetConnection();
                cn.Open();

                using var cmd = new OracleCommand("sp_insertar_cliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(":p_nrodoc", c.nrodoc);
                cmd.Parameters.Add(":p_nombre", c.nombre);
                cmd.Parameters.Add(":p_apellido", c.apellido);

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

        public bool Actualizar(Clientes c)
        {
            try
            {
                using var cn = OracleConnectionFactory.GetConnection();
                cn.Open();

                using var transaction = cn.BeginTransaction();
                using (var cmdVal = new OracleCommand(
                    "SELECT COUNT(*) FROM CLIENTE WHERE NRODOC = :nrodoc AND CODCLIENTE <> :codcliente", cn))
                {
                    cmdVal.Parameters.Add(":nrodoc", c.nrodoc);
                    cmdVal.Parameters.Add(":codcliente", c.codcliente);
                    int count = Convert.ToInt32(cmdVal.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Ya existe cliente con este nro documento.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                using var cmd = new OracleCommand(
                    @"UPDATE CLIENTE SET
                        nombre = :nombre,
                        apellido = :apellido
                     WHERE codcliente = :codcliente", cn);

                cmd.Parameters.Add(":nombre", c.nombre);
                cmd.Parameters.Add(":apellido", c.apellido);
                cmd.Parameters.Add(":codcliente", c.codcliente);

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
        public bool Eliminar(int codcliente)
        {
            if (codcliente <= 0)
            {
                MessageBox.Show("Seleccione un cliente válido para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using var cn = OracleConnectionFactory.GetConnection();
                cn.Open();

                using var transaction = cn.BeginTransaction();

                using var cmd = new OracleCommand(
                    "DELETE FROM CLIENTE WHERE CODCLIENTE = :codcliente", cn);
                cmd.Parameters.Add(":codcliente", codcliente);
                cmd.Transaction = transaction;

                int filasAfectadas = cmd.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    MessageBox.Show("No se encontró el cliente a eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    transaction.Rollback();
                    return false;
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
