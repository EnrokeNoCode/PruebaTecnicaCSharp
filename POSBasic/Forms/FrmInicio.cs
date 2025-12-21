using Microsoft.Extensions.DependencyInjection;
using POSBasic.Forms;

namespace POSBasic
{
    public partial class FrmInicio : Form
    {
        private readonly IServiceProvider _provider;
        public FrmInicio(IServiceProvider provider)
        {
            InitializeComponent();
            _provider = provider;
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var f = _provider.GetRequiredService<FrmClientes>();
            f.ShowDialog();
        }

        private void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var f = _provider.GetRequiredService<FrmProductos>();
            f.ShowDialog();
        }

        private void ventaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var f = _provider.GetRequiredService<FrmVentas>();
            f.ShowDialog();
        }

        private void FrmInicio_Load(object sender, EventArgs e)
        {

        }
    }
}
