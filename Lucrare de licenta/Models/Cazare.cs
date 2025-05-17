using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("cazari")]
    public class Cazare
    {
        [Key]
        [Column("cod_cazare")]
        public int cod_cazare { get; set; }

        [Required]
        [StringLength(100)]
        [Column("den_cazare", TypeName = "nvarchar(100)")]
        public string den_cazare { get; set; }

        [StringLength(500)]
        [Column("desc_cazare", TypeName = "desc_cazare")]
        public string? desc_cazare { get; set; }

        [Required]
        [Range(1, 5)]
        [Column("nr_stele", TypeName = "tinyint")]
        public byte nr_stele { get; set; }

        [Required]
        [Range(1, 3)]
        [Column("tip_cazare", TypeName = "tinyint")]
        public byte tip_cazare { get; set; }

        [StringLength(100)]
        [Column("adresa_cazare", TypeName = "nvarchar(100)")]
        public string? adresa_cazare { get; set; }

        [Required]
        [Column("check_in_inceput", TypeName = "time")]
        public TimeSpan check_in_inceput { get; set; }

        [Column("check_in_final", TypeName = "time")]
        public TimeSpan? check_in_final { get; set; }

        [Required]
        [Column("check_out", TypeName = "time")]
        public TimeSpan check_out { get; set; }

        [Required]
        [ForeignKey("Destinatie")]
        [Column("cod_destinatie", TypeName = "smallint")]
        public short cod_destinatie { get; set; }

        public virtual Destinatie? Destinatie { get; set; }
    }
}
