using ApiRestSistema.Interfaces;
using ApiRestSistema.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestSistema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaRepositorio _ventaRepositorio;

        public VentaController(IVentaRepositorio ventaRepositorio)
        {
            _ventaRepositorio = ventaRepositorio;
        }

        // Endpoint para registrar una venta
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] Venta venta)
        {
            if (venta == null)
            {
                return BadRequest("La venta no es válida.");
            }

            try
            {
                var ventaGenerada = await _ventaRepositorio.Registrar(venta);
                return Ok(ventaGenerada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al registrar la venta.", detalle = ex.Message });
            }
        }

        // Endpoint para obtener el historial de ventas
        [HttpGet("historial")]
        public async Task<IActionResult> Historial([FromQuery] string buscarPor, [FromQuery] string numeroVenta, [FromQuery] string fechaInicio, [FromQuery] string fechaFin)
        {
            // Log o depuración para verificar el valor de numeroVenta
            Console.WriteLine($"numeroVenta: {numeroVenta}");

            try
            {
                // Si se busca por fecha, no es necesario número de venta
                if (buscarPor == "fecha" && string.IsNullOrEmpty(numeroVenta))
                {
                    numeroVenta = null; // No es necesario pasar un valor para número de venta
                }

                var historial = await _ventaRepositorio.Historial(buscarPor, numeroVenta, fechaInicio, fechaFin);
                return Ok(historial);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener el historial de ventas.", detalle = ex.Message });
            }
        }

        // Endpoint para generar un reporte de ventas
        [HttpGet("reporte")]
        public async Task<IActionResult> Reporte([FromQuery] string fechaInicio, [FromQuery] string fechaFin)
        {
            if (string.IsNullOrWhiteSpace(fechaInicio) || string.IsNullOrWhiteSpace(fechaFin))
            {
                return BadRequest("Las fechas de inicio y fin son obligatorias.");
            }

            try
            {
                var reporte = await _ventaRepositorio.Reporte(fechaInicio, fechaFin);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al generar el reporte.", detalle = ex.Message });
            }
        }

        [HttpPost("registrar-detalle")]
        public async Task<IActionResult> RegistrarDetalle([FromBody] DetalleVenta detalle)
        {
            if (detalle == null)
            {
                return BadRequest("El detalle de venta no es válido.");
            }

            try
            {
                var detalleRegistrado = await _ventaRepositorio.RegistrarDetalle(detalle);
                return Ok(detalleRegistrado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al registrar el detalle de venta.", detalle = ex.Message });
            }
        }
    }

}
