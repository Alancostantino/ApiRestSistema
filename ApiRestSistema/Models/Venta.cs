
namespace ApiRestSistema.Models
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public string NumeroVenta { get; set; } = string.Empty;
        public DateTime? FechaRegistro { get; set; } = DateTime.Now;
        public int IdVehiculo { get; set; }
        public string DescripcionVehiculo { get; set; } = string.Empty;
        public decimal PrecioVenta { get; set; }
        public string TipoPago { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;

        // Relación con la tabla Vehiculos (Product)
        public Product? Vehiculo { get; set; }
    }
}