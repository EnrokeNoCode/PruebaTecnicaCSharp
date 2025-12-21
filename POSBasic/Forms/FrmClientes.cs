using POSBasic.Models;
using POSBasic.Services.Interfaces;

namespace POSBasic.Forms
{
    public partial class FrmClientes : Form
    {
        private readonly IClienteService _service;
        public FrmClientes(IClienteService service)
        {
            InitializeComponent();
            _service = service;
            InhabilitarCampos();
            txtCodCliente.Visible = false;
            txtNroDoc.KeyPress += Txt_KeyPress_Enter;
            txtNombre.KeyPress += Txt_KeyPress_Enter;
            txtApellido.KeyPress += Txt_KeyPress_Enter;
        }
        private void Txt_KeyPress_Enter(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            ValidarBotonesInicio();
            dgvClientes.DataSource = _service.Listar();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ValidarBotonesNuevo();
            HabilitarCampos();
            txtNroDoc.Focus();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            var cliente = new Clientes
            {
                nrodoc = txtNroDoc.Text.Trim(),
                nombre = txtNombre.Text.Trim(),
                apellido = txtApellido.Text.Trim()
            };

            if (_service.Insertar(cliente))
            {
                MessageBox.Show("Cliente insertado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvClientes.DataSource = _service.Listar();
                LimpiarForm();
                InhabilitarCampos();
                ValidarBotonesInicio();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarForm();
            InhabilitarCampos();
            ValidarBotonesInicio();
            dgvClientes.DataSource = _service.Listar();
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HabilitarCampos();
            txtNroDoc.Enabled = false; // Este es unico tambien ya que no puede existir dos clientes con un mismo nro documento
            txtNombre.Focus();
            ValidarBotonesEditarEliminar();
            if (e.RowIndex < 0) return;
            var row = dgvClientes.Rows[e.RowIndex];
            txtCodCliente.Tag = Convert.ToInt32(row.Cells["codcliente"].Value);
            txtNroDoc.Text = row.Cells["nrodoc"].Value.ToString();
            txtNombre.Text = row.Cells["nombre"].Value.ToString();
            txtApellido.Text = row.Cells["apellido"].Value.ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if ((int)txtCodCliente.Tag == 0)
            {
                MessageBox.Show("Seleccione un cliente de la lista primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidarCampos()) return;
            var cliente = new Clientes
            {
                codcliente = (int)txtCodCliente.Tag,
                nrodoc = txtNroDoc.Text.Trim(),
                nombre = txtNombre.Text.Trim(),
                apellido = txtApellido.Text.Trim()
            };
            if (_service.Actualizar(cliente))
            {
                MessageBox.Show("Cliente actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvClientes.DataSource = _service.Listar();
                InhabilitarCampos();
                LimpiarForm();
                ValidarBotonesInicio();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if ((int)txtCodCliente.Tag == 0)
            {
                MessageBox.Show("Seleccione un cliente de la lista primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resultado = MessageBox.Show("¿Desea eliminar el cliente seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado != DialogResult.Yes) return;

            if (_service.Eliminar((int)txtCodCliente.Tag, out string error))
            {
                MessageBox.Show("Cliente eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvClientes.DataSource = _service.Listar();
                txtCodCliente.Tag = 0;
                LimpiarForm();
                InhabilitarCampos();
                ValidarBotonesInicio();
            }
            else
            {
                MessageBox.Show("Error al eliminar: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*funciones y validaciones*/

        //Limpiar formulario y actualizar estados de botones dependiendo de la accion
        private void LimpiarForm()
        {
            txtNroDoc.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
        }

        private void HabilitarCampos()
        {
            txtNroDoc.Enabled = true;
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
        }

        private void InhabilitarCampos()
        {
            txtNroDoc.Enabled = false;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
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
            //Validaciones de Campos una vez realizando el proceso de guardado controlamos
            if (string.IsNullOrWhiteSpace(txtNroDoc.Text))
            {
                MessageBox.Show("El Nro de Documento es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNroDoc.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            return true;
        }
    }
}
