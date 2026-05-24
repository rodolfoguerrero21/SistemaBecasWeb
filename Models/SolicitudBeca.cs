using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaBecasWeb.Models
{
    public class SolicitudBeca
    {
        [Key]
        public int IdSolicitud { get; set; }

        [Required(ErrorMessage = "El nombre del estudiante es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Nombre del Estudiante")]
        public string NombreEstudiante { get; set; } = string.Empty;

        [Required(ErrorMessage = "El RUT es obligatorio")]
        [StringLength(12)]
        [Display(Name = "RUT")]
        public string Rut { get; set; } = string.Empty;

        [Required(ErrorMessage = "La carrera es obligatoria")]
        [StringLength(100)]
        [Display(Name = "Carrera")]
        public string Carrera { get; set; } = string.Empty;

        [Required(ErrorMessage = "El promedio de notas es obligatorio")]
        [Range(1.0, 7.0, ErrorMessage = "El promedio debe estar entre 1.0 y 7.0")]
        [Display(Name = "Promedio de Notas")]
        [Column(TypeName = "decimal(3,1)")]
        public decimal PromedioNotas { get; set; }

        [Required(ErrorMessage = "El ingreso familiar es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El ingreso familiar debe ser mayor o igual a 0")]
        [Display(Name = "Ingreso Familiar Mensual")]
        public int IngresoFamiliar { get; set; }

        [Required(ErrorMessage = "Los integrantes del grupo familiar son obligatorios")]
        [Range(1, int.MaxValue, ErrorMessage = "Los integrantes deben ser mayor a 0")]
        [Display(Name = "Integrantes del Grupo Familiar")]
        public int IntegrantesFamilia { get; set; }

        [Required(ErrorMessage = "La situación laboral es obligatoria")]
        [Display(Name = "Situación Laboral")]
        public string SituacionLaboral { get; set; } = string.Empty;

        [Display(Name = "Puntaje")]
        public int Puntaje { get; set; }

        [Display(Name = "Resultado")]
        public string Resultado { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado de solicitud es obligatorio")]
        [Display(Name = "Estado de Solicitud")]
        public string EstadoSolicitud { get; set; } = "Pendiente";

        [Display(Name = "Fecha de Solicitud")]
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;
    }
}