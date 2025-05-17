using Lucrare_de_licenta.Models;
using Microsoft.EntityFrameworkCore;
namespace Adventour.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {
        }
        public DbSet<Beneficiar> beneficiari { get; set; }
        public DbSet<Camera> camere { get; set; }
        public DbSet<Categorie> categorii { get; set; }
        public DbSet<Cazare> cazari { get; set; }
        public DbSet<Destinatie> destinatii { get; set; }
        public DbSet<Destinatie_itinerariu> destinatii_itinerarii { get; set; }
        public DbSet<Factura> facturi { get; set; }
        public DbSet<Itinerariu> itinerarii { get; set; }
        public DbSet<Oferta> oferte { get; set; }
        public DbSet<Plata> plati { get; set; }
        public DbSet<Punct_Plecare> puncte_plecare { get; set; }
        public DbSet<Rezervare> rezervari { get; set; }
        public DbSet<Tara> tari { get; set; }
        public DbSet<Tur> tururi { get; set; }
        public DbSet<Tur_categorie> tur_categorii { get; set; }
        public DbSet<Utilizator> utilizatori { get; set; }
    }
}

