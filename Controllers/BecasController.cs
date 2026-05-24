using Microsoft.AspNetCore.Mvc;
using SistemaBecasWeb.Models;
using SistemaBecasWeb.Services;

namespace SistemaBecasWeb.Controllers
{
    public class BecasController : Controller
    {
        private readonly BecaService _becaService;

        public BecasController(BecaService becaService)
        {
            _becaService = becaService;
        }

        public IActionResult Index()
        {
            var solicitudes = _becaService.ListarSolicitudes();
            return View(solicitudes);
        }

        public IActionResult Detalle(int id)
        {
            var solicitud = _becaService.ObtenerPorId(id);
            if (solicitud == null)
            {
                TempData["Error"] = "Solicitud no encontrada.";
                return RedirectToAction("Index");
            }
            return View(solicitud);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(SolicitudBeca solicitud)
        {
            if (!ModelState.IsValid)
                return View(solicitud);

            if (_becaService.ExisteRut(solicitud.Rut))
            {
                ModelState.AddModelError("Rut", "Ya existe una solicitud con este RUT.");
                return View(solicitud);
            }

            _becaService.RegistrarSolicitud(solicitud);
            TempData["Mensaje"] = "Solicitud registrada exitosamente.";
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            var solicitud = _becaService.ObtenerPorId(id);
            if (solicitud == null)
            {
                TempData["Error"] = "Solicitud no encontrada.";
                return RedirectToAction("Index");
            }
            return View(solicitud);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(SolicitudBeca solicitud)
        {
            if (!ModelState.IsValid)
                return View(solicitud);

            if (_becaService.ExisteRut(solicitud.Rut, solicitud.IdSolicitud))
            {
                ModelState.AddModelError("Rut", "Ya existe otra solicitud con este RUT.");
                return View(solicitud);
            }

            bool ok = _becaService.ActualizarSolicitud(solicitud);
            if (!ok)
            {
                TempData["Error"] = "No se pudo actualizar la solicitud.";
                return RedirectToAction("Index");
            }

            TempData["Mensaje"] = "Solicitud actualizada exitosamente.";
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            var solicitud = _becaService.ObtenerPorId(id);
            if (solicitud == null)
            {
                TempData["Error"] = "Solicitud no encontrada.";
                return RedirectToAction("Index");
            }
            return View(solicitud);
        }

        [HttpPost, ActionName("EliminarConfirmado")]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarConfirmado(int id)
        {
            bool ok = _becaService.EliminarSolicitud(id);
            TempData["Mensaje"] = ok
                ? "Solicitud eliminada exitosamente."
                : "No se pudo eliminar la solicitud.";
            return RedirectToAction("Index");
        }
    }
}