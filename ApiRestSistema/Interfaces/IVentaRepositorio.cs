using ApiRestSistema.Models;

namespace ApiRestSistema.Interfaces
{
    public interface IVentaRepositorio
    {
        Task<Venta> Registrar(Venta venta); // Registrar una nueva venta
        Task<List<Venta>> Historial(string numeroVenta, string fechaInicio, string fechaFin); // Obtener historial de ventas
        

    }

}