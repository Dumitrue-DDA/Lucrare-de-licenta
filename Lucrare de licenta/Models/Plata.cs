using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("Plati")]
    public class Plata
    {
        [Key]
        [Column("cod_plata")]
        public int cod_plata { get; set; }

        [Required]
        [Column("status_plata", TypeName = "tinyint")]
        public byte status_plata { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal suma_plata { get; set; }

        [Required]
        [Column("tip_plata", TypeName = "bit")]
        public bool tip_plata { get; set; } = false; // 0 = integral, 1 = in rate

        [Required]
        [Column("termen_plata", TypeName = "date")]
        public DateOnly termen_plata { get; set; }

        [Column("nr_rate", TypeName = "tinyint")]

        public byte? nr_rate { get; set; }

        [Required]
        [Column("creat_la", TypeName = "datetime")]
        public DateTime creat_la { get; set; } = DateTime.Now;

        [Column("modificat_la", TypeName = "datetime")]
        public DateTime? modificat_la { get; set; }

        [MaxLength(255)]
        [Column("stripe_charge_id", TypeName = "nvarchar(255)")]
        public string? stripe_intent_id { get; set; } = null;

        [MaxLength(3)]
        [Column("stripe_currency", TypeName = "nvarchar(3)")]
        public string? stripe_method_id { get; set; } = null;

        [ForeignKey("Rezervare")]
        [Column("cod_rezervare")]
        public int cod_rezervare { get; set; }

        public virtual Rezervare? Rezervare { get; set; }
    }
}
