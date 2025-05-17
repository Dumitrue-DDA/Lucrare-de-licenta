using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("categorii")]
    public class Categorie
    {
        [Key]
        [Column("cod_categ", TypeName = "tinyint")]
        public byte cod_categ { get; set; }

        [Required]
        [StringLength(100)]
        [Column("den_categ", TypeName = "nvarchar(100)")]
        public string den_categ { get; set; }

        [StringLength(250)]
        [Column("desc_categ", TypeName = "nvarchar(250)")]
        public string? desc_categ { get; set; }

        [Required]
        [StringLength(250)]
        [Column("img_categ", TypeName = "nvarchar(250)")]
        public string img_categ { get; set; }
    }
}
