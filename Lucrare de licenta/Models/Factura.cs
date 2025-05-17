using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("facturi")]
    public class Factura
    {
        [Key]
        [Column("cod_factura")]
        public int cod_factura { get; set; }

        [Required]
        [Range(0, 4, ErrorMessage = "Statusul facturii trebuie sa fie intre 0 si 4")]
        [Column("status_factura", TypeName = "tinyint")]
        public byte status_factura { get; set; } = 0;

        [Required]
        [Column("suma_factura", TypeName = "decimal(10,2)")]
        public decimal suma_factura { get; set; }

        [DataType(DataType.Date)]
        [Column("data_achitarii", TypeName = "date")]
        public DateOnly data_achitarii { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [MaxLength(255)]
        [Column("stripe_charge_id", TypeName = "nvarchar(255)")]
        public string? stripe_charge_id { get; set; } = null;

        [MaxLength(3)]
        [Column("stripe_currency", TypeName = "nvarchar(3)")]
        public string stripe_currency { get; set; } = "RON";

        [Column("nr_rata", TypeName = "tinyint")]
        public byte? nr_rata { get; set; }

        [ForeignKey("Plata")]
        [Column("cod_plata")]
        public int cod_plata { get; set; }

        public virtual Plata? Plata { get; set; }
    }
}
