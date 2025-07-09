using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Adventour.Data
{
    /// <summary>
    /// Contextul Bazei de date. Ofera acces la modelele ce reprezinta tabelele si permite
    /// efectuarea de interogari asupra acestora.
    /// </summary>
    /// <remarks>
    /// Extinde clasa <see cref="IdentityDbContext{TUser, TRole, TKey}"/> pentru a integra
    /// functionalitatea librariei Core Identity.
    /// Dam override la metoda <see cref="OnModelCreating(ModelBuilder)"/> pentru a defini
    /// secventele si pentru atribuirea manuala a tabelelor implicite Identity. 
    /// Astfel ne asiguram ca modelele corespund corect tabelelor din baza de date.
    /// </remarks>
    public class AppDbContext : IdentityDbContext<Utilizator, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        { }
        public DbSet<Utilizator> utilizatori { get; set; }
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
        public DbSet<IdentityRole<int>> Roluri { get; set; }
        public DbSet<IdentityUserRole<int>> UtilizatorRoluri { get; set; }
        public DbSet<IdentityUserClaim<int>> CereriUtilizator { get; set; }
        public DbSet<IdentityUserLogin<int>> LoginuriUtilizator { get; set; }
        public DbSet<IdentityUserToken<int>> TokenuriUtilizator { get; set; }
        public DbSet<IdentityRoleClaim<int>> CereriRol { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // SECVENTE
            builder.HasSequence<byte>("seq_tari", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
            builder.HasSequence<short>("seq_destinatii", schema: "dbo").
                StartsAt(1)
                .IncrementsBy(1);
            builder.HasSequence<int>("seq_utilizatori", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
            builder.HasSequence<int>("seq_cazari", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
            builder.HasSequence<int>("seq_tururi", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
            builder.HasSequence<byte>("seq_categorii", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
            builder.HasSequence<int>("seq_tururi_categorii", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
            builder.HasSequence<int>("seq_itinerarii", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
            builder.HasSequence<int>("seq_destinatii_itinerarii", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
            builder.HasSequence<byte>("seq_puncte_plecare", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
            builder.HasSequence<int>("seq_oferte", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
            builder.HasSequence<int>("seq_rezervari", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
            builder.HasSequence<int>("seq_camere", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
            builder.HasSequence<int>("seq_beneficiari", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
            builder.HasSequence<int>("seq_plati", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
            builder.HasSequence<int>("seq_facturi", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);

            // Atribuirea entitatilor la tabelele si coloanele potrivite
            var user = builder.Entity<Utilizator>();
            user.ToTable("utilizatori");
            user.Property(e => e.Id).HasColumnName("nr_utilizator");
            user.Property(e => e.UserName).HasColumnName("nume");
            user.Property(e => e.NormalizedUserName).HasColumnName("nume_normalizat");
            user.Property(e => e.Email).HasColumnName("email");
            user.Property(e => e.NormalizedEmail).HasColumnName("email_normalizat");
            user.Property(e => e.EmailConfirmed).HasColumnName("email_confirmat");
            user.Property(e => e.PasswordHash).HasColumnName("parola");
            user.Property(e => e.SecurityStamp).HasColumnName("token_securitate");
            user.Property(e => e.ConcurrencyStamp).HasColumnName("token_concurenta");
            user.Property(e => e.PhoneNumber).HasColumnName("nr_telefon");
            user.Property(e => e.PhoneNumberConfirmed).HasColumnName("telefon_confirmat");
            user.Property(e => e.TwoFactorEnabled).HasColumnName("auth_doi_factori");
            user.Property(e => e.LockoutEnd).HasColumnName("blocare_pana_la");
            user.Property(e => e.LockoutEnabled).HasColumnName("blocare_activata");
            user.Property(e => e.AccessFailedCount).HasColumnName("incercari_esuate");

            // Construirea tabelelor pe baza claselor Identity<int>
            builder.Entity<IdentityRole<int>>().ToTable("Roluri");
            builder.Entity<IdentityUserRole<int>>().ToTable("UtilizatorRoluri");
            builder.Entity<IdentityUserClaim<int>>().ToTable("CereriUtilizator");
            builder.Entity<IdentityUserLogin<int>>().ToTable("LoginuriUtilizator");
            builder.Entity<IdentityUserToken<int>>().ToTable("TokenuriUtilizator");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("CereriRol");
        }

    }
}

