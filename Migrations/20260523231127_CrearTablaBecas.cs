using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaBecasWeb.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaBecas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolicitudesBeca",
                columns: table => new
                {
                    IdSolicitud = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEstudiante = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rut = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Carrera = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PromedioNotas = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    IngresoFamiliar = table.Column<int>(type: "int", nullable: false),
                    IntegrantesFamilia = table.Column<int>(type: "int", nullable: false),
                    SituacionLaboral = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Puntaje = table.Column<int>(type: "int", nullable: false),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoSolicitud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesBeca", x => x.IdSolicitud);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitudesBeca");
        }
    }
}
