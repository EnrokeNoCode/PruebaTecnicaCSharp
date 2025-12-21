using System.Data;

namespace POSBasic.Services.Interfaces
{
    public interface IBuscadorService
    {
        DataTable Listar(
            string nombreTabla,
            string campoCodigo,
            string campoDescripcion,
            string campoExtra1 = "",
            string campoExtra2 = "",
            string campoExtra3 = "",
            string whereCond = "");
    }
}
