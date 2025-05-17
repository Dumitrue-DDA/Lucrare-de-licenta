using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("tururi_categorii")]
    public class Tur_categorie
    {
        [Key]
        [Column("cod_tur_categ")]
        public int cod_tur_categ { get; set; }

        [ForeignKey("Tur")]
        [Column("cod_tur")]
        public int cod_tur { get; set; }

        [ForeignKey("Categorie")]
        [Column("cod_categ", TypeName = "tinyint")]
        public byte cod_categ { get; set; }

        public virtual Tur? Tur { get; set; }
        public virtual Categorie? Categorie { get; set; }
    }
}
