using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("destinatii")]
    public class Destinatie
    {
        [Key]
        [Column("cod_destinatie", TypeName = "smallint")]
        public short cod_destinatie { get; set; }

        [Required]
        [StringLength(100)]
        [Column("den_destinatie", TypeName = "nvarchar(100)")]
        public string den_destinatie { get; set; }

        [Required]
        [StringLength(250)]
        [Column("img_destinatie", TypeName = "nvarchar(250)")]
        public string img_destinatie { get; set; }

        [StringLength(100)]
        [Column("judet", TypeName = "nvarchar(100)")]
        public string? judet { get; set; } = null;

        [StringLength(500)]
        [Column("desc_destinatie", TypeName = "nvarchar(500)")]
        public string? desc_destinatie { get; set; } = null;

        [ForeignKey("Tara")]
        [Column("cod_tara", TypeName = "tinyint")]
        public byte cod_tara { get; set; }

        public virtual Tara Tara { get; set; } = null!;
    }
}
