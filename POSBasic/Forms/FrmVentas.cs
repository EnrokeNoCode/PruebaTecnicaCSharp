
using POSBasic.Models;
using POSBasic.Services;
using POSBasic.Utils;
using System.Data;


namespace POSBasic.Forms
{
    public partial class FrmVentas : Form
    {
        private VentaService _ventaService = new VentaService();
        private DataTable dtDetalle;
        private int _filaSeleccionada = -1;
        private DataTable dtVentas;
        private int indiceVenta = -1;
        private int _editar = 0;

        public FrmVentas()
        {
            InitializeComponent();
            InhabilitarCampos();
            txtCodVenta.Visible = false;
            txtNumVenta.KeyPress += Txt_KeyPress_Enter;
            txtCliente.KeyPress += Txt_KeyPress_Enter;
            txtCodigoBarra.KeyPress += Txt_KeyPress_Enter;
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            InicializarDetalle();
            CargarVentas();
            ValidarBotonesInicio();
        }
        private void Txt_KeyPress_Enter(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtCodigoBarra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                AbrirBuscadorProducto();
                e.Handled = true;
            }
        }
        private void txtCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                AbrirBuscadorCliente();
                e.Handled = true;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ValidarBotonesNuevo();
            HabilitarCampos();
            LimpiarForm();
            txtFechaVenta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtNumVenta.Focus();
        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            if (!ValidarDetalle()) return;

            int codProducto = Convert.ToInt32(txtCodigoBarra.Tag);
            decimal precio = Convert.ToDecimal(txtPrecioVenta.Text);
            decimal cantidad = Convert.ToDecimal(txtCantidad.Text);

            // Buscar si ya existe el producto
            DataRow filaExistente = dtDetalle.AsEnumerable()
                .FirstOrDefault(r => r.Field<int>("CodProducto") == codProducto);

            if (filaExistente != null)
            {
                // SUMA cantidad
                filaExistente["Cantidad"] =
                    filaExistente.Field<decimal>("Cantidad") + cantidad;

                filaExistente["Subtotal"] =
                    filaExistente.Field<decimal>("Cantidad") *
                    filaExistente.Field<decimal>("Precio");
            }
            else
            {
                DataRow row = dtDetalle.NewRow();
                row["CodProducto"] = codProducto;
                row["CodigoBarra"] = txtCodigoBarra.Text;
                row["Descripcion"] = txtDescripcion.Text;
                row["Precio"] = precio;
                row["Cantidad"] = cantidad;
                row["Subtotal"] = precio * cantidad;

                dtDetalle.Rows.Add(row);
            }

            RecalcularTotal();
            LimpiarDetalle();
            txtCodigoBarra.Focus();
        }

        private void dgvVentasDet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            _filaSeleccionada = e.RowIndex;

            DataRow row = dtDetalle.Rows[e.RowIndex];

            txtCodigoBarra.Tag = row["CodProducto"];
            txtCodigoBarra.Text = row["CodigoBarra"].ToString();
            txtDescripcion.Text = row["Descripcion"].ToString();
            txtPrecioVenta.Text = row["Precio"].ToString();
            txtCantidad.Text = row["Cantidad"].ToString();
        }

        private void btnEliminarDetalle_Click(object sender, EventArgs e)
        {
            if (_filaSeleccionada < 0)
            {
                MessageBox.Show("Seleccione una línea");
                return;
            }

            dtDetalle.Rows.RemoveAt(_filaSeleccionada);
            _filaSeleccionada = -1;

            RecalcularTotal();
            LimpiarDetalle();
        }

        //Para poder hacer el recorrido de pantallas uno en uno
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (indiceVenta < dtVentas.Rows.Count - 1)
            {
                indiceVenta++;
                MostrarVentaActual();
            }
        }
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (indiceVenta > 0)
            {
                indiceVenta--;
                MostrarVentaActual();
            }
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            InhabilitarCampos();
            ValidarBotonesInicio();
            LimpiarForm();
            CargarVentas();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Para no pisar procesos funciona como un booleano 
            // 0 es guardar normal 1 es editar solo el detalle
            if (_editar == 1)
            {
                if (dtDetalle.Rows.Count == 0)
                {
                    MessageBox.Show("La venta no tiene detalle");
                    return;
                }

                try
                {
                    int codventa = Convert.ToInt32(txtCodVenta.Tag);
                    _ventaService.ActualizarDetalleVenta(codventa, dtDetalle);

                    MessageBox.Show($"Detalle de la venta Nº {codventa} actualizado correctamente");
                    _editar = 0;
                    InhabilitarCampos();
                    ValidarBotonesInicio();
                    LimpiarDetalle();
                    CargarVentas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo actualizar la venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } 
            else 
            {
                if (!ValidarCampos()) return;
                if (dtDetalle.Rows.Count == 0)
                {
                    MessageBox.Show("La venta no tiene detalle");
                    return;
                }

                try
                {
                    int codventa = _ventaService.InsertarVenta(
                        txtNumVenta.Text,
                        Convert.ToInt32(txtCliente.Tag),
                        dtDetalle
                    );

                    MessageBox.Show($"Venta registrada correctamente Nº {codventa}");
                    InhabilitarCampos();
                    ValidarBotonesInicio();
                    LimpiarForm();
                    CargarVentas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo registrar la venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodVenta.Tag == null || Convert.ToInt32(txtCodVenta.Tag) == 0)
            {
                MessageBox.Show("No hay venta seleccionada para eliminar.");
                return;
            }

            int codventa = Convert.ToInt32(txtCodVenta.Tag);

            var result = MessageBox.Show(
                $"¿Está seguro que desea eliminar la venta Nº {txtNumVenta.Text}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    _ventaService.EliminarVenta(codventa);

                    MessageBox.Show("Venta eliminada correctamente.");
                    LimpiarForm();
                    CargarVentas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la venta: " + ex.Message);
                }
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtCodVenta.Tag == null || Convert.ToInt32(txtCodVenta.Tag) == 0)
            {
                MessageBox.Show("Seleccione una venta para editar.");
                return;
            }

            ValidarBotonesEditarEliminar();
        }

        /* funciones y validaciones */
        private void AbrirBuscadorCliente()
        {
            using FrmBuscar f = new FrmBuscar(
                nombreTabla: "CLIENTE C ",
                campoCodigo: "C.CODCLIENTE",
                campoDescripcion: "C.NRODOC",
                campoExtra1: "C.NOMBRE || ', ' || C.APELLIDO",
                whereCond: "1 = 1"
            );

            if (f.ShowDialog() == DialogResult.OK)
            {
                txtCliente.Tag = f.Resultado["Codigo"].ToString();
                txtCliente.Text = f.Resultado["Descripcion"].ToString();
                if (f.Resultado.Table.Columns.Contains("CampoExtra1"))
                    lblCliente.Text = f.Resultado["CampoExtra1"].ToString();
                txtCodigoBarra.Focus();
            }
        }
        private void AbrirBuscadorProducto()
        {
            using FrmBuscar f = new FrmBuscar(
                nombreTabla: "PRODUCTO P ",
                campoCodigo: "P.CODPRODUCTO",
                campoDescripcion: "P.CODIGOBARRA",
                campoExtra1: "P.DESPRODUCTO",
                campoExtra2: "P.PRECIOVENTA",
                campoExtra3: "P.STOCK",
                whereCond: "1 = 1"
            );

            if (f.ShowDialog() == DialogResult.OK)
            {
                txtCodigoBarra.Tag = f.Resultado["Codigo"].ToString();
                txtCodigoBarra.Text = f.Resultado["Descripcion"].ToString();
                if (f.Resultado.Table.Columns.Contains("CampoExtra1"))
                    txtDescripcion.Text = f.Resultado["CampoExtra1"].ToString();
                if (f.Resultado.Table.Columns.Contains("CampoExtra2"))
                    txtPrecioVenta.Text = f.Resultado["CampoExtra2"].ToString();
                txtCantidad.Text = "1";
                txtCantidad.Focus();
            }
        }
        private void LimpiarForm()
        {
            txtNumVenta.Text = string.Empty;
            txtFechaVenta.Text = string.Empty;

            txtCliente.Tag = 0;
            txtCliente.Text = string.Empty;
            lblCliente.Text = string.Empty;

            txtTotalVenta.Text = "0.00";
            dtDetalle.Clear();
            LimpiarDetalle();
            _filaSeleccionada = -1;
        }

        private void LimpiarDetalle()
        {
            txtDescripcion.Text = string.Empty;
            txtCodigoBarra.Text = string.Empty;
            txtCodigoBarra.Tag = 0;
            txtPrecioVenta.Text = string.Empty;
            txtCantidad.Text = string.Empty;

        }

        private void HabilitarCampos()
        {
            txtFechaVenta.Enabled = false;
            txtCliente.Enabled = true;
            txtNumVenta.Enabled = true;
            txtCodigoBarra.Enabled = true;
            txtDescripcion.Enabled = false;
            txtPrecioVenta.Enabled = false;
            txtCantidad.Enabled = true;
            txtPrecioVenta.Enabled = false;
            txtTotalVenta.Enabled = false;
        }

        private void InhabilitarCampos()
        {
            txtFechaVenta.Enabled = false;
            txtCliente.Enabled = false;
            txtNumVenta.Enabled = false;
            txtCodigoBarra.Enabled = false;
            txtDescripcion.Enabled = false;
            txtPrecioVenta.Enabled = false;
            txtCantidad.Enabled = false;
            txtPrecioVenta.Enabled = false;
            txtTotalVenta.Enabled = false;
        }

        private void ValidarBotonesEditarEliminar()
        {
            txtCodigoBarra.Enabled = true;
            txtCantidad.Enabled = true;
            btnAgregarDetalle.Enabled = true;
            btnEliminarDetalle.Enabled = true;
            dgvVentasDet.Enabled = true;
            btnGuardar.Enabled = true;
            _editar = 1;

            txtNumVenta.Enabled = false;
            txtCliente.Enabled = false;
            txtFechaVenta.Enabled = false;

            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            btnAgregarDetalle.Enabled = true;
            btnEliminarDetalle.Enabled = true;
        }

        private void ValidarBotonesInicio()
        {
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnGuardar.Enabled = false;
            btnSiguiente.Enabled = true;
            btnAnterior.Enabled = true;
            btnNuevo.Enabled = true;
            btnAgregarDetalle.Enabled = false;
            btnEliminarDetalle.Enabled= false;
        }
        private void ValidarBotonesNuevo()
        {
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;
            btnSiguiente.Enabled = false;
            btnAnterior.Enabled = false;
            btnNuevo.Enabled = false;
            btnAgregarDetalle.Enabled = true;
            btnEliminarDetalle.Enabled = true;
        }

        private void InicializarDetalle()
        {
            dtDetalle = new DataTable();

            dtDetalle.Columns.Add("CodProducto", typeof(int));
            dtDetalle.Columns.Add("CodigoBarra", typeof(string));
            dtDetalle.Columns.Add("Descripcion", typeof(string));
            dtDetalle.Columns.Add("Precio", typeof(decimal));
            dtDetalle.Columns.Add("Cantidad", typeof(decimal));
            dtDetalle.Columns.Add("Subtotal", typeof(decimal));

            dgvVentasDet.DataSource = dtDetalle;

            dgvVentasDet.Columns["CodProducto"].Visible = false;
        }

        private bool ValidarDetalle()
        {
            if (txtCodigoBarra.Tag == null || Convert.ToInt32(txtCodigoBarra.Tag) == 0)
            {
                MessageBox.Show("Seleccione un producto");
                return false;
            }

            if (!decimal.TryParse(txtCantidad.Text, out decimal cant) || cant <= 0)
            {
                MessageBox.Show("Cantidad inválida");
                txtCantidad.Focus();
                return false;
            }

            return true;
        }

        private void RecalcularTotal()
        {
            decimal total = 0;

            foreach (DataRow row in dtDetalle.Rows)
                total += row.Field<decimal>("Subtotal");

            txtTotalVenta.Text = total.ToString("N2");
        }

        private void MostrarVentaActual()
        {
            if (indiceVenta < 0 || indiceVenta >= dtVentas.Rows.Count)
                return;

            int codventa = Convert.ToInt32(dtVentas.Rows[indiceVenta]["codventa"]);
            DataTable cab = _ventaService.ObtenerCabecera(codventa);
            if (cab.Rows.Count == 0) return;

            DataRow r = cab.Rows[0];
            txtCodVenta.Tag = codventa;
            txtNumVenta.Text = r["numventa"].ToString();
            txtFechaVenta.Text = Convert.ToDateTime(r["fechaventa"]).ToString("dd/MM/yyyy");

            txtCliente.Tag = r["codcliente"];
            txtCliente.Text = r["nrodoc"].ToString();
            lblCliente.Text = r["cliente"].ToString();

            txtTotalVenta.Text = Convert.ToDecimal(r["totalventa"]).ToString("N2");
            dtDetalle.Clear();
            DataTable det = _ventaService.ObtenerDetalle(codventa);

            foreach (DataRow d in det.Rows)
            {
                DataRow row = dtDetalle.NewRow();

                row["CodProducto"] = d["codproducto"];
                row["CodigoBarra"] = d["codigobarra"];
                row["Descripcion"] = d["desproducto"];
                row["Precio"] = d["precio"];
                row["Cantidad"] = d["cantidad"];
                row["Subtotal"] = d["subtotal"];

                dtDetalle.Rows.Add(row);
            }
        }
        private void CargarVentas()
        {
            dtVentas = _ventaService.ListarCodVentas();

            if (dtVentas.Rows.Count == 0)
                return;

            indiceVenta = 0;
            MostrarVentaActual();
        }

        private bool ValidarCampos()
        {
            //Validaciones de Campos una vez realizando el proceso de guardado controlamos
            if (string.IsNullOrWhiteSpace(txtCliente.Text))
            {
                MessageBox.Show("El Cliente es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCliente.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumVenta.Text))
            {
                MessageBox.Show("El Nro de Venta es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumVenta.Focus();
                return false;
            }
            return true;
        }
    }
}
