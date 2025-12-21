using POSBasic.Models;
using System.Data;

namespace POSBasic.Services.Interfaces
{
    public interface IClienteService
    {
        DataTable Listar();
        bool Insertar(Clientes cliente);
        bool Actualizar(Clientes cliente);
        bool Eliminar(int codCliente, out string mensajeError);
    }
}
