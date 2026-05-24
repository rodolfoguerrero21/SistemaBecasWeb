using SistemaBecasWeb.Data;
using SistemaBecasWeb.Models;

namespace SistemaBecasWeb.Services
{
    public class BecaService
    {
        private readonly AppDbContext _context;

        public BecaService(AppDbContext context)
        {
            _context = context;
        }

        public int CalcularPuntaje(decimal promedioNotas, int ingresoFamiliar, int integrantesFamilia, string situacionLaboral)
        {
            int puntaje = 0;

            if (promedioNotas >= 6.0m)
                puntaje += 40;
            else if (promedioNotas >= 5.0m)
                puntaje += 30;
            else if (promedioNotas >= 4.0m)
                puntaje += 15;

            decimal perCapita = integrantesFamilia > 0
                ? (decimal)ingresoFamiliar / integrantesFamilia
                : ingresoFamiliar;

            if (perCapita <= 200000)
                puntaje += 40;
            else if (perCapita <= 400000)
                puntaje += 25;
            else
                puntaje += 10;

            if (situacionLaboral == "Trabaja")
                puntaje += 10;

            return puntaje;
        }

        public string DeterminarResultado(int puntaje)
        {
            if (puntaje >= 70)
                return "Recomendada";
            else if (puntaje >= 50)
                return "En revisión";
            else
                return "No recomendada";
        }

        public List<SolicitudBeca> ListarSolicitudes()
        {
            return _context.SolicitudesBeca
                .OrderByDescending(s => s.FechaSolicitud)
                .ToList();
        }

        public SolicitudBeca? ObtenerPorId(int id)
        {
            return _context.SolicitudesBeca.Find(id);
        }

        public void RegistrarSolicitud(SolicitudBeca solicitud)
        {
            solicitud.Puntaje = CalcularPuntaje(
                solicitud.PromedioNotas,
                solicitud.IngresoFamiliar,
                solicitud.IntegrantesFamilia,
                solicitud.SituacionLaboral);

            solicitud.Resultado = DeterminarResultado(solicitud.Puntaje);
            solicitud.FechaSolicitud = DateTime.Now;

            _context.SolicitudesBeca.Add(solicitud);
            _context.SaveChanges();
        }

        public bool ActualizarSolicitud(SolicitudBeca solicitud)
        {
            var existente = _context.SolicitudesBeca.Find(solicitud.IdSolicitud);
            if (existente == null) return false;

            existente.NombreEstudiante = solicitud.NombreEstudiante;
            existente.Rut = solicitud.Rut;
            existente.Carrera = solicitud.Carrera;
            existente.PromedioNotas = solicitud.PromedioNotas;
            existente.IngresoFamiliar = solicitud.IngresoFamiliar;
            existente.IntegrantesFamilia = solicitud.IntegrantesFamilia;
            existente.SituacionLaboral = solicitud.SituacionLaboral;
            existente.EstadoSolicitud = solicitud.EstadoSolicitud;

            existente.Puntaje = CalcularPuntaje(
                existente.PromedioNotas,
                existente.IngresoFamiliar,
                existente.IntegrantesFamilia,
                existente.SituacionLaboral);
            existente.Resultado = DeterminarResultado(existente.Puntaje);

            _context.SaveChanges();
            return true;
        }

        public bool EliminarSolicitud(int id)
        {
            var solicitud = _context.SolicitudesBeca.Find(id);
            if (solicitud == null) return false;

            _context.SolicitudesBeca.Remove(solicitud);
            _context.SaveChanges();
            return true;
        }

        public bool ExisteRut(string rut, int? excluirId = null)
        {
            return _context.SolicitudesBeca.Any(s =>
                s.Rut == rut &&
                (excluirId == null || s.IdSolicitud != excluirId));
        }
    }
}