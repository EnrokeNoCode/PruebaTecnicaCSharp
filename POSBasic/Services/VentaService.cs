using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using POSBasic.Database;
using System.Data;

namespace POSBasic.Services
{
    public class VentaService
    {
        //Recuperamos los codventas para poder realizar el recorrido en la pantalla de ventas
        public DataTable ListarCodVentas()
        {
            var dt = new DataTable();
            using var cn = OracleConnectionFactory.GetConnection();
            cn.Open();

            using var da = new OracleDataAdapter(
                @"SELECT codventa
                  FROM venta
                  ORDER BY codventa DESC", cn);

            da.Fill(dt);
            return dt;
        }
        //Funcion para Mostrar Ventas cuando recorremos el formulario
        public DataTable ObtenerCabecera(int codventa)
        {
            var dt = new DataTable();
            using var cn = OracleConnectionFactory.GetConnection();
            cn.Open();

            using var da = new OracleDataAdapter(
                @"SELECT v.codventa,
                         v.fechaventa,
                         v.numventa,
                         v.codcliente,
                         c.nrodoc,
                         c.nombre || ', ' || c.apellido AS cliente,
                         v.totalventa
                  FROM venta v
                  JOIN cliente c ON c.codcliente = v.codcliente
                  WHERE v.codventa = :id", cn);

            da.SelectCommand.Parameters.Add(":id", codventa);
            da.Fill(dt);
            return dt;
        }
        
        public DataTable ObtenerDetalle(int codventa)
        {
            var dt = new DataTable();
            try
            {
                using var cn = OracleConnectionFactory.GetConnection();
                using var da = new OracleDataAdapter(@"
                        SELECT d.codproducto, p.codigobarra, p.desproducto,
                               d.cantidadventa AS cantidad, d.precioventa AS precio,
                               d.totallinea AS subtotal
                        FROM ventadet d
                        JOIN producto p ON p.codproducto = d.codproducto
                        WHERE d.codventa = :id
                        ORDER BY d.numlinea", cn);

                da.SelectCommand.Parameters.Add(":id", OracleDbType.Int32).Value = codventa;

                cn.Open();
                da.Fill(dt);
            }
            catch (OracleException ex)
            {
                throw new Exception("Error al consultar el detalle en la base de datos: " + ex.Message);
            }
            return dt;
        }

        public int InsertarVenta(string numventa, int codcliente, DataTable dtDetalle)
        {
            int codventa = 0;

            using var cn = OracleConnectionFactory.GetConnection();
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                //Insertar cabecera
                using (var cmdCab = new OracleCommand("SP_INSERTAR_VENTA_CAB", cn))
                {
                    cmdCab.CommandType = CommandType.StoredProcedure;
                    cmdCab.Transaction = tran;

                    cmdCab.Parameters.Add("p_numventa", OracleDbType.Varchar2).Value = numventa;
                    cmdCab.Parameters.Add("p_codcliente", OracleDbType.Int32).Value = codcliente;
                    var pOut = new OracleParameter("p_codventa_out", OracleDbType.Int32, ParameterDirection.Output);
                    cmdCab.Parameters.Add(pOut);

                    cmdCab.ExecuteNonQuery();
                    codventa = Convert.ToInt32(pOut.Value.ToString());
                }

                decimal totalVenta = 0;

                //Insertar detalle
                foreach (DataRow row in dtDetalle.Rows)
                {
                    int codProducto = Convert.ToInt32(row["CodProducto"]);
                    decimal cantidad = Convert.ToDecimal(row["Cantidad"]);
                    decimal precio = Convert.ToDecimal(row["Precio"]);
                    decimal subtotal = cantidad * precio;

                    using var cmdDet = new OracleCommand("SP_INSERTAR_VENTA_DET", cn)
                    {
                        CommandType = CommandType.StoredProcedure,
                        Transaction = tran
                    };

                    cmdDet.Parameters.Add("p_codventa", OracleDbType.Int32).Value = codventa;
                    cmdDet.Parameters.Add("p_codproducto", OracleDbType.Int32).Value = codProducto;
                    cmdDet.Parameters.Add("p_cantidad", OracleDbType.Decimal).Value = cantidad;
                    cmdDet.Parameters.Add("p_precio", OracleDbType.Decimal).Value = precio;

                    cmdDet.ExecuteNonQuery();

                    totalVenta += subtotal;
                }

                // Actualizar total de la venta
                using var cmdTotal = new OracleCommand(
                    "UPDATE venta SET totalventa = :total WHERE codventa = :codventa", cn);
                cmdTotal.Transaction = tran;
                cmdTotal.Parameters.Add("total", OracleDbType.Decimal).Value = totalVenta;
                cmdTotal.Parameters.Add("codventa", OracleDbType.Int32).Value = codventa;
                cmdTotal.ExecuteNonQuery();

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBox.Show("Error al insertar la venta: " + ex.Message);
                return 0;
            }
            return codventa;
        }

        public void ActualizarDetalleVenta(int codventa, DataTable dtDetalle)
        {
            using var cn = OracleConnectionFactory.GetConnection();
            cn.Open();

            try
            {
                using var cmd = new OracleCommand("SP_ACTUALIZAR_DETALLE_VENTA", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("p_codventa", OracleDbType.Int32).Value = codventa;

                // Convertir DataTable a JSON simple
                string jsonDetalle = JsonConvert.SerializeObject(
                    dtDetalle.AsEnumerable().Select(r => new {
                        codproducto = r.Field<int>("CodProducto"),
                        precio = r.Field<decimal>("Precio"),
                        cantidad = r.Field<decimal>("Cantidad"),
                        subtotal = r.Field<decimal>("Subtotal")
                    })
                );

                cmd.Parameters.Add("p_jsondetalle", OracleDbType.Clob).Value = jsonDetalle;

                cmd.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                throw new Exception("Error en la actualización de la venta: " + ex.Message);
            }
        }


        public void EliminarVenta(int codventa)
        {
            using var cn = OracleConnectionFactory.GetConnection();
            cn.Open();
            try
            {
                using var cmd = new OracleCommand("SP_ELIMINAR_VENTA", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("p_codventa", OracleDbType.Int32).Value = codventa;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la venta: " + ex.Message);
            }
        }

    }
}
