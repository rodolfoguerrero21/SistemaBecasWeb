using Microsoft.AspNetCore.Mvc;
using SistemaBecasWeb.Services;

namespace SistemaBecasWeb.Controllers
{
    [Route("api/becas")]
    [ApiController]
    public class ApiBecasController : ControllerBase
    {
        private readonly BecaService _becaService;

        public ApiBecasController(BecaService becaService)
        {
            _becaService = becaService;
        }

        [HttpPost("evaluar")]
        public IActionResult Evaluar([FromBody] EvaluacionRequest request)
        {
            if (request == null)
                return BadRequest(new { error = "Datos inválidos." });

            if (request.PromedioNotas < 1.0m || request.PromedioNotas > 7.0m)
                return BadRequest(new { error = "El promedio debe estar entre 1.0 y 7.0" });

            if (request.IngresoFamiliar < 0)
                return BadRequest(new { error = "El ingreso familiar debe ser mayor o igual a 0" });

            if (request.IntegrantesFamilia <= 0)
                return BadRequest(new { error = "Los integrantes deben ser mayor a 0" });

            int puntaje = _becaService.CalcularPuntaje(
                request.PromedioNotas,
                request.IngresoFamiliar,
                request.IntegrantesFamilia,
                request.SituacionLaboral);

            string resultado = _becaService.DeterminarResultado(puntaje);

            return Ok(new { puntaje, resultado });
        }

        [HttpGet]
        public IActionResult ObtenerTodas()
        {
            var solicitudes = _becaService.ListarSolicitudes();
            return Ok(solicitudes);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var solicitud = _becaService.ObtenerPorId(id);
            if (solicitud == null)
                return NotFound(new { error = $"Solicitud con ID {id} no encontrada." });

            return Ok(solicitud);
        }
    }

    public class EvaluacionRequest
    {
        public decimal PromedioNotas { get; set; }
        public int IngresoFamiliar { get; set; }
        public int IntegrantesFamilia { get; set; }
        public string SituacionLaboral { get; set; } = string.Empty;
    }
}