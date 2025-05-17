using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("tururi")]
    public class Tur
    {
        [Key]
        [Column("cod_tur")]
        public int cod_tur { get; set; }

        [Required]
        [StringLength(100)]
        [Column("den_tur", TypeName = "nvarchar(100)")]
        public string den_tur { get; set; }

        [StringLength(500)]
        [Column("desc_tur", TypeName = "nvarchar(500)")]
        public string? desc_tur { get; set; }

        [Range(1, 5)]
        [Required]
        [Column("solicitare_fizica", TypeName = "tinyint")]
        public byte sol_fiz { get; set; }

        [Required]
        [Column("grup_minim", TypeName = "tinyint")]
        public byte grup_minim { get; set; }

        [Required]
        [Column("grup_maxim", TypeName = "tinyint")]
        public byte grup_maxim { get; set; }

        [Required]
        [StringLength(250)]
        [Column("img_tur", TypeName = "nvarchar(250)")]
        public string img_tur { get; set; }
    }
}
