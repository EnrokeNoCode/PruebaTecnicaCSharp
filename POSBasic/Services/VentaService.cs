using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using POSBasic.Persistence.Interface;
using POSBasic.Services.Interfaces;
using System.Data;

namespace POSBasic.Services
{
    public class VentaService : IVentaService
    {
        private readonly IConnectionFactory _connectionFactory;

        public VentaService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public DataTable ListarCodVentas()
        {
            var dt = new DataTable();
            try
            {
                using var cn = _connectionFactory.GetConnection();
                cn.Open();

                using var cmd = new OracleCommand("SP_LISTAR_COD_VENTAS", cn)
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
                throw new Exception("Error al listar ventas.", ex);
            }
        }

        public DataTable ObtenerCabecera(int codventa)
        {
            var dt = new DataTable();
            try
            {
                using var cn = _connectionFactory.GetConnection();
                cn.Open();

                using var cmd = new OracleCommand("SP_OBTENER_CABECERA_VENTA", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("p_codventa", OracleDbType.Int32).Value = codventa;
                cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor)
                             .Direction = ParameterDirection.Output;

                using var da = new OracleDataAdapter(cmd);
                da.Fill(dt);

                return dt;
            }
            catch (OracleException ex)
            {
                throw new Exception($"Error al obtener cabecera de la venta {codventa}.", ex);
            }
        }
        public DataTable ObtenerDetalle(int codventa)
        {
            var dt = new DataTable();
            try
            {
                using var cn = _connectionFactory.GetConnection();
                cn.Open();

                using var cmd = new OracleCommand("SP_OBTENER_DETALLE_VENTA", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("p_codventa", OracleDbType.Int32).Value = codventa;
                cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor)
                             .Direction = ParameterDirection.Output;

                using var da = new OracleDataAdapter(cmd);
                da.Fill(dt);

                return dt;
            }
            catch (OracleException ex)
            {
                throw new Exception("Error al obtener detalle de la venta.", ex);
            }
        }
        public int InsertarVenta(string numventa, int codcliente, DataTable dtDetalle)
        {
            using var cn = _connectionFactory.GetConnection();
            cn.Open();
            using var tran = cn.BeginTransaction();
            try
            {
                int codventa;
                using (var cmdCab = new OracleCommand("SP_INSERTAR_VENTA_CAB", cn))
                {
                    cmdCab.CommandType = CommandType.StoredProcedure;
                    cmdCab.Transaction = tran;

                    cmdCab.Parameters.Add("p_numventa", numventa);
                    cmdCab.Parameters.Add("p_codcliente", codcliente);

                    var pOut = new OracleParameter("p_codventa_out", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmdCab.Parameters.Add(pOut);
                    cmdCab.ExecuteNonQuery();
                    codventa = ((OracleDecimal)pOut.Value).ToInt32();
                }
                foreach (DataRow row in dtDetalle.Rows)
                {
                    // Convertimos de forma segura
                    int codProducto = Convert.ToInt32(row["CodProducto"]);
                    decimal cantidad = Convert.ToDecimal(row["Cantidad"]);
                    decimal precio = Convert.ToDecimal(row["Precio"]);

                    using var cmdDet = new OracleCommand("SP_INSERTAR_VENTA_DET", cn)
                    {
                        CommandType = CommandType.StoredProcedure,
                        Transaction = tran
                    };

                    cmdDet.Parameters.Add("p_codventa", codventa);
                    cmdDet.Parameters.Add("p_codproducto", codProducto);
                    cmdDet.Parameters.Add("p_cantidad", cantidad);
                    cmdDet.Parameters.Add("p_precio", precio);

                    cmdDet.ExecuteNonQuery();
                }
                tran.Commit();

                return codventa;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception("Error al insertar la venta.", ex);
            }
        }

        public void ActualizarDetalleVenta(int codventa, DataTable dtDetalle)
        {
            using var cn = _connectionFactory.GetConnection();
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                using (var cmdClear = new OracleCommand("SP_LIMPIAR_DETALLE_VENTA", cn))
                {
                    cmdClear.CommandType = CommandType.StoredProcedure;
                    cmdClear.Transaction = tran;
                    cmdClear.Parameters.Add("p_codventa", codventa);
                    cmdClear.ExecuteNonQuery();
                }

                foreach (DataRow row in dtDetalle.Rows)
                {
                    using var cmdDet = new OracleCommand("SP_ACTUALIZAR_VENTA_DET", cn)
                    {
                        CommandType = CommandType.StoredProcedure,
                        Transaction = tran
                    };

                    cmdDet.Parameters.Add("p_codventa", codventa);
                    cmdDet.Parameters.Add("p_codproducto", row["CodProducto"]);
                    cmdDet.Parameters.Add("p_cantidad", row["Cantidad"]);
                    cmdDet.Parameters.Add("p_precio", row["Precio"]);
                    cmdDet.ExecuteNonQuery();
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception("Error al actualizar detalle de venta.", ex);
            }
        }
        public void EliminarVenta(int codventa)
        {
            try
            {
                using var cn = _connectionFactory.GetConnection();
                cn.Open();

                using var cmd = new OracleCommand("SP_ELIMINAR_VENTA", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("p_codventa", codventa);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la venta.", ex);
            }
        }
    }
}
