using System.Data;

namespace POSBasic.Services.Interfaces
{
    public interface IVentaService
    {
        DataTable ListarCodVentas();
        DataTable ObtenerCabecera(int codventa);
        DataTable ObtenerDetalle(int codventa);

        int InsertarVenta(string numventa, int codcliente, DataTable dtDetalle);
        void ActualizarDetalleVenta(int codventa, DataTable dtDetalle);
        void EliminarVenta(int codventa);
    }
}
