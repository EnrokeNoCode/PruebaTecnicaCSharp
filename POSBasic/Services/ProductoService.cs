using POSBasic.Models;
using POSBasic.Services.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using POSBasic.Persistence.Interface;

namespace POSBasic.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IConnectionFactory _connectionFactory;

        public ProductoService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public DataTable Listar()
        {
            var dt = new DataTable();
            try
            {
                using var cn = _connectionFactory.GetConnection();
                cn.Open();

                using var cmd = new OracleCommand("SP_LISTAR_PRODUCTOS", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor)
                             .Direction = ParameterDirection.Output;

                using var da = new OracleDataAdapter(cmd);
                da.Fill(dt);

                return dt;
            }
            catch (OracleException ex)
            {
                throw new Exception("Error al listar productos.", ex);
            }
        }
        public bool Insertar(Productos p)
        {
            try
            {
                using var cn = _connectionFactory.GetConnection();
                cn.Open();

                using var cmd = new OracleCommand("sp_insertar_producto", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(":p_codigobarra", p.codigobarra);
                cmd.Parameters.Add(":p_desproducto", p.desproducto);
                cmd.Parameters.Add(":p_precioventa", p.precioventa);
                cmd.Parameters.Add(":p_stock", p.stock);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (OracleException ex)
            {
                throw new Exception("Error al insertar producto.", ex);
            }
        }
        public bool Actualizar(Productos p)
        {
            using var cn = _connectionFactory.GetConnection();
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
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
                tran.Rollback();
                throw new Exception("Error al actualizar producto.", ex);
            }
        }
        public bool Eliminar(int codproducto, out string mensajeError)
        {
            mensajeError = string.Empty;
            using var cn = _connectionFactory.GetConnection();
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
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
