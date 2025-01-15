using ApiRestSistema.Models;

namespace ApiRestSistema.Interfaces
{
    public interface IVentaRepositorio
    {
        Task<Venta> Registrar(Venta entidad);
        Task<List<Venta>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin);
        Task<List<DetalleVenta>> Reporte(string FechaInicio, string FechaFin);
        Task<DetalleVenta> RegistrarDetalle(DetalleVenta detalle);
    }
}
