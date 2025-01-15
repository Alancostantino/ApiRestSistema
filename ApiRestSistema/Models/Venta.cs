namespace ApiRestSistema.Models
{
    public class Venta
    {
      
        public int IdVenta { get; set; }
        public string? NumeroDocumento { get; set; } // Número de factura o documento único
        public string? TipoPago { get; set; } // Efectivo o Transferencia
        public DateTime? FechaRegistro { get; set; } = DateTime.Now;
        public decimal? Total { get; set; }
        public virtual ICollection<DetalleVenta> DetalleVenta { get; } = new List<DetalleVenta>();
    }
}
