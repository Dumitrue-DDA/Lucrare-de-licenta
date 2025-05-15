using Lucrare_de_licenta.Models;
using Microsoft.EntityFrameworkCore;
namespace Adventour.Data
{
    public class AdventourContext : DbContext
    {
        public AdventourContext(DbContextOptions<AdventourContext> options) :
            base(options)
        {
        }
        public DbSet<Tara> tari { get; set; }
    }
}

