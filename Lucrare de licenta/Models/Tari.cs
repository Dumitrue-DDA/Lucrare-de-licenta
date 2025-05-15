using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("tari")]
    public class Tara
    {
        [Key]
        [Column("cod_tara")]
        public int cod_tara { get; set; }

        [Required]
        [Column("den_tara")]
        [StringLength(50)]
        public required string den_tara { get; set; }

        [Required]
        [Column("cod_continent")]
        [Range(1, 6)]
        public required int continent { get; set; }

        [Required]
        [Column("img_tara")]
        [StringLength(250)]
        public required string img_tara { get; set; }

        [Column("desc_tara")]
        [StringLength(500)]
        public string? desc_tara { get; set; }

    }
}
