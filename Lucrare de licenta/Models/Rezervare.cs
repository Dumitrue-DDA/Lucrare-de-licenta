using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("Rezervari")]
    public class Rezervare
    {
        [Key]
        [Column("cod_rezervare")]
        public int cod_rezervare { get; set; }

        [Required]
        [Column("data_rezervare", TypeName = "date")]
        public DateOnly data_rezervare { get; set; }

        [Required]
        [Range(1, 4)]
        [Column("status_rezervare", TypeName = "tinyint")]
        public byte status_rezervare { get; set; } = 1;

        [ForeignKey("Oferta")]
        [Column("cod_oferta")]
        public int cod_oferta { get; set; }

        [ForeignKey("Utilizator")]
        [Column("nr_utilizator")]
        public int? nr_utilizator { get; set; } = null;

        public virtual Oferta? Oferta { get; set; }
        public virtual Utilizator? Utilizator { get; set; }
    }
}
