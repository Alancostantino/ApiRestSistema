namespace ApiRestSistema.Models
{
    public class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }

        public int? IdVenta { get; set; }

        public int? IdProducto { get; set; }

        public int? Cantidad { get; set; }

        public decimal? Precio { get; set; }

        public decimal? Total { get; set; }

        public virtual Product? IdProductoNavigation { get; set; }

        public virtual Venta? IdVentaNavigation { get; set; }
    }
}
