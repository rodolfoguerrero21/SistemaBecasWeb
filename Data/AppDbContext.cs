using Microsoft.EntityFrameworkCore;
using SistemaBecasWeb.Models;

namespace SistemaBecasWeb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<SolicitudBeca> SolicitudesBeca { get; set; }
    }
}