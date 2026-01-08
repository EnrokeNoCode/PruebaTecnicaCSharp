using Oracle.ManagedDataAccess.Client;
using POSBasic.Models;
using POSBasic.Persistence.Interface;
using POSBasic.Services.Interfaces;
using System.Data;

namespace POSBasic.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IConnectionFactory _connectionFactory;

        public ClienteService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public DataTable Listar()
        {
            try
            {
                using var cn = _connectionFactory.GetConnection();
                cn.Open();

                using var cmd = new OracleCommand("PKG_CLIENTE.LISTAR", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor)
                              .Direction = ParameterDirection.Output;

                var dt = new DataTable();
                using var da = new OracleDataAdapter(cmd);
                da.Fill(dt);

                return dt;
            }
            catch (OracleException ex)
            {
                throw new Exception("Error al listar clientes.", ex);
            }
        }

        public bool Insertar(Clientes c)
        {
            try
            {
                using var cn = _connectionFactory.GetConnection();
                cn.Open();

                using var cmd = new OracleCommand("PKG_CLIENTE.INSERTAR", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(":p_nrodoc", c.nrodoc);
                cmd.Parameters.Add(":p_nombre", c.nombre);
                cmd.Parameters.Add(":p_apellido", c.apellido);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (OracleException ex)
            {
                throw new Exception("Error al insertar cliente.", ex);
            }
        }

        public bool Actualizar(Clientes c)
        {
            try
            {
                using var cn = _connectionFactory.GetConnection();
                cn.Open();
                using var tran = cn.BeginTransaction();

                using var cmd = new OracleCommand("PKG_CLIENTE.ACTUALIZAR", cn)
                {
                    CommandType = CommandType.StoredProcedure,
                    Transaction = tran
                };

                cmd.Parameters.Add("p_codcliente", c.codcliente);
                cmd.Parameters.Add("p_nrodoc", c.nrodoc);
                cmd.Parameters.Add("p_nombre", c.nombre);
                cmd.Parameters.Add("p_apellido", c.apellido);

                cmd.ExecuteNonQuery();
                tran.Commit();
                return true;
            }
            catch (OracleException ex)
            {
                throw new Exception("Error al actualizar cliente.", ex);
            }
        }

        public bool Eliminar(int codcliente, out string mensajeError)
        {
            mensajeError = string.Empty;
            using var cn = _connectionFactory.GetConnection();
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                using var cmd = new OracleCommand("PKG_CLIENTE.ELIMINAR", cn)
                {
                    CommandType = CommandType.StoredProcedure,
                    Transaction = tran
                };

                cmd.Parameters.Add("p_codcliente", codcliente);
                cmd.ExecuteNonQuery();

                tran.Commit();
                return true;
            }
            catch (OracleException ex)
            {
                tran.Rollback();
                mensajeError = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                mensajeError = "Error inesperado: " + ex.Message;
                return false;
            }
        }

    }
}
