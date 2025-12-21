using POSBasic.Models;
using System.Data;

namespace POSBasic.Services.Interfaces
{
    public interface IProductoService
    {
        DataTable Listar();
        bool Insertar(Productos producto);
        bool Actualizar(Productos producto);
        bool Eliminar(int codProducto, out string mensajeError);
    }
}
