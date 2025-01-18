using ApiRestSistema.Interfaces;
using ApiRestSistema.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarVenta([FromBody] Venta venta)
        {
            if (venta == null) return BadRequest("La venta no puede ser nula.");

            try
            {
                var ventaRegistrada = await _ventaRepositorio.Registrar(venta);
                return CreatedAtAction(nameof(RegistrarVenta), new { id = ventaRegistrada.IdVenta }, ventaRegistrada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("historial")]
        public async Task<IActionResult> Historial(string numeroVenta = null, string fechaInicio = null, string fechaFin = null)
        {
            try
            {
                var historial = await _ventaRepositorio.Historial(numeroVenta, fechaInicio, fechaFin);
                return Ok(historial);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        
    }
}