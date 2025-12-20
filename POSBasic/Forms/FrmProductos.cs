using POSBasic.Models;
using POSBasic.Services;

namespace POSBasic.Forms
{
    public partial class FrmProductos : Form
    {
        private ProductoService _service = new ProductoService();
        public FrmProductos()
        {
            InitializeComponent();
            InhabilitarCampos();
            txtCodProducto.Visible = false;
            txtDescripcion.KeyPress += Txt_KeyPress_Enter;
            txtCodigoBarra.KeyPress += Txt_KeyPress_Enter;
            txtPrecioVenta.KeyPress += Txt_KeyPress_Enter;
            txtStock.KeyPress += Txt_KeyPress_Enter;
        }

        private void Txt_KeyPress_Enter(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        private void FrmProductos_Load(object sender, EventArgs e)
        {
            ValidarBotonesInicio();
            dgvProductos.DataSource = _service.Listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ValidarBotonesNuevo();
            HabilitarCampos();
            txtCodigoBarra.Focus();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            var producto = new Productos
            {
                codigobarra = txtCodigoBarra.Text.Trim(),
                desproducto = txtDescripcion.Text.Trim(),
                precioventa = decimal.Parse(txtPrecioVenta.Text),
                stock = decimal.Parse(txtStock.Text)
            };

            if (_service.Insertar(producto))
            {
                MessageBox.Show("Producto insertado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvProductos.DataSource = _service.Listar();
                LimpiarForm();
                InhabilitarCampos();
                ValidarBotonesInicio();
            }
        }
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HabilitarCampos();
            txtCodigoBarra.Enabled = false; // Este es unico tambien ya que no puede existir dos productos con un mismo codigo barra
            txtDescripcion.Focus();
            ValidarBotonesEditarEliminar();
            if (e.RowIndex < 0) return;
            var row = dgvProductos.Rows[e.RowIndex];
            txtCodProducto.Tag = Convert.ToInt32(row.Cells["codproducto"].Value);
            txtCodigoBarra.Text = row.Cells["codigobarra"].Value.ToString();
            txtDescripcion.Text = row.Cells["desproducto"].Value.ToString();
            txtPrecioVenta.Text = row.Cells["precioventa"].Value.ToString();
            txtStock.Text = row.Cells["stock"].Value.ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if ((int)txtCodProducto.Tag == 0)
            {
                MessageBox.Show("Seleccione un producto de la lista primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidarCampos()) return;
            var producto = new Productos
            {
                codproducto = (int)txtCodProducto.Tag,
                codigobarra = txtCodigoBarra.Text.Trim(),
                desproducto = txtDescripcion.Text.Trim(),
                precioventa = decimal.Parse(txtPrecioVenta.Text),
                stock = decimal.Parse(txtStock.Text)
            };
            if (_service.Actualizar(producto))
            {
                MessageBox.Show("Producto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvProductos.DataSource = _service.Listar();
                InhabilitarCampos();
                LimpiarForm();
                ValidarBotonesInicio();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarForm();
            InhabilitarCampos();
            ValidarBotonesInicio();
            dgvProductos.DataSource = _service.Listar();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if ((int)txtCodProducto.Tag == 0)
            {
                MessageBox.Show("Seleccione un producto de la lista primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resultado = MessageBox.Show("¿Desea eliminar el producto seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado != DialogResult.Yes) return;

            if (_service.Eliminar((int)txtCodProducto.Tag))
            {
                MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvProductos.DataSource = _service.Listar();
                txtCodProducto.Tag = 0;
                LimpiarForm();
                InhabilitarCampos();
                ValidarBotonesInicio();
            }
        }

        /*funciones y validaciones*/
        //Limpiar formulario y actualizar estados de botones dependiendo de la accion
        private void LimpiarForm()
        {
            txtCodigoBarra.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtPrecioVenta.Text = string.Empty;
            txtStock.Text = string.Empty;
        }

        private void HabilitarCampos()
        {
            txtCodigoBarra.Enabled = true;
            txtDescripcion.Enabled = true;
            txtPrecioVenta.Enabled = true;
            txtStock.Enabled = true;
        }

        private void InhabilitarCampos()
        {
            txtCodigoBarra.Enabled = false;
            txtDescripcion.Enabled = false;
            txtPrecioVenta.Enabled = false;
            txtStock.Enabled = false;
        }

        private void ValidarBotonesEditarEliminar()
        {
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnGuardar.Enabled = false;
            btnNuevo.Enabled = false;
        }

        private void ValidarBotonesInicio()
        {
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
            btnNuevo.Enabled = true;
        }
        private void ValidarBotonesNuevo()
        {
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;
            btnNuevo.Enabled = false;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("La descripción del producto es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecioVenta.Text))
            {
                MessageBox.Show("El precio de venta es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioVenta.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrecioVenta.Text, out _))
            {
                MessageBox.Show("El precio de venta debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioVenta.Focus();
                return false;
            }

            if (!decimal.TryParse(txtStock.Text, out _))
            {
                MessageBox.Show("El stock debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return false;
            }

            return true;
        }
    }
}
