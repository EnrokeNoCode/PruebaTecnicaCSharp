
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
                using var da = new OracleDataAdapter("SELECT codcliente, nrodoc, nombre, apellido FROM cliente", cn);
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

                using var tran = cn.BeginTransaction();

                using var cmd = new OracleCommand("SP_ACTUALIZAR_CLIENTE", cn)
                {
                    CommandType = CommandType.StoredProcedure,
                    Transaction = tran
                };

                cmd.Parameters.Add("p_codcliente", OracleDbType.Int32).Value = c.codcliente;
                cmd.Parameters.Add("p_nrodoc", OracleDbType.Varchar2).Value = c.nrodoc;
                cmd.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = c.nombre;
                cmd.Parameters.Add("p_apellido", OracleDbType.Varchar2).Value = c.apellido;

                cmd.ExecuteNonQuery();

                tran.Commit();
                return true;
            }
            catch (OracleException ex)
            {
                MessageBox.Show( ex.Message, "Validación", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show( "Error al actualizar cliente: " + ex.Message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Eliminar(int codcliente)
        {
            try
            {
                using var cn = OracleConnectionFactory.GetConnection();
                cn.Open();

                using var tran = cn.BeginTransaction();
                using var cmd = new OracleCommand("SP_ELIMINAR_CLIENTE", cn)
                {
                    CommandType = CommandType.StoredProcedure,
                    Transaction = tran
                };
                cmd.Parameters.Add("p_codcliente", OracleDbType.Int32).Value = codcliente;

                cmd.ExecuteNonQuery();
                tran.Commit();

                return true;
            }
            catch (OracleException ex)
            {
                MessageBox.Show( ex.Message, "Error",MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar cliente: " + ex.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}
