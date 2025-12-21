using POSBasic.Services;
using POSBasic.Services.Interfaces;
using System.Data;

namespace POSBasic.Utils
{
    public partial class FrmBuscar : Form
    {
        public DataRow Resultado { get; private set; } = null;
        private readonly IBuscadorService _service;

        // Variables que indican qué tabla y campos vamos a mostrar
        private string _nombreTabla;
        private string _campoCodigo;
        private string _campoDescripcion;
        private string _campoExtra1;
        private string _campoExtra2;
        private string _campoExtra3;
        private string _whereCond;

        // Constructor del form, se reciben los campos de la tabla a buscar
        public FrmBuscar(IBuscadorService service,
            string nombreTabla,
            string campoCodigo,
            string campoDescripcion,
            string campoExtra1 = "",
            string campoExtra2 = "",
            string campoExtra3 = "",
            string whereCond = "")
        {
            InitializeComponent();
            _service = service;

            // Guardamos los parámetros en las variables internas
            _nombreTabla = nombreTabla;
            _campoCodigo = campoCodigo;
            _campoDescripcion = campoDescripcion;
            _campoExtra1 = campoExtra1;
            _campoExtra2 = campoExtra2;
            _campoExtra3 = campoExtra3;
            _whereCond = whereCond;
        }

        // Cuando se carga el form, traemos los datos
        private void FrmBuscar_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        // Método que pide los datos al servicio y los muestra en el DataGridView
        private void CargarDatos()
        {
            DataTable dt = _service.Listar(
                _nombreTabla,
                _campoCodigo,
                _campoDescripcion,
                _campoExtra1,
                _campoExtra2,
                _campoExtra3,
                _whereCond);

            dgvResultado.DataSource = dt; // Bind de la tabla al grid
        }

        // Filtra los datos en tiempo real según lo que escriba el usuario
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (dgvResultado.DataSource is DataTable dt)
            {
                string filtro = txtBuscar.Text.Trim().Replace("'", "''"); // Escapamos comillas

                if (string.IsNullOrEmpty(filtro))
                {
                    dt.DefaultView.RowFilter = ""; // Sin filtro
                }
                else
                {
                    if (int.TryParse(filtro, out int cod))
                    {
                        // Si el usuario escribió un número, filtramos por código o descripción
                        dt.DefaultView.RowFilter = $"Codigo = {cod} OR Descripcion LIKE '%{filtro}%'";
                    }
                    else
                    {
                        // Si es texto, solo por descripción
                        dt.DefaultView.RowFilter = $"Descripcion LIKE '%{filtro}%'";
                    }

                }
            }
        }

        // Cuando el usuario hace doble click en una fila, seleccionamos esa fila
        private void dgvResultado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtenemos el DataRow de la fila seleccionada
                DataRowView rowView = (DataRowView)dgvResultado.Rows[e.RowIndex].DataBoundItem;
                Resultado = rowView.Row;

                // Cerramos el form con OK para que el form padre sepa que seleccionó algo
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
