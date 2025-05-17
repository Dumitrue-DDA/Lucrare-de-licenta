using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("oferte")]
    public class Oferta
    {
        [Key]
        [Column("cod_oferta")]
        public int cod_oferta { get; set; }

        [Required]
        [Column("tip_transport", TypeName = "bit")]
        public bool tip_transport { get; set; }

        [Required]
        [Column("pret_adult", TypeName = "decimal(10,2)")]
        public decimal pret_adult { get; set; }

        [Column("pret_copil", TypeName = "decimal(10,2)")]
        public decimal pret_copil { get; set; }

        [Required]
        [Column("data_plecare", TypeName = "date")]
        public DateOnly data_plecare { get; set; }

        [Required]
        [Column("data_intoarcere", TypeName = "date")]
        public DateOnly data_intoarcere { get; set; }

        [Column("loc_libere", TypeName = "tinyint")]
        public byte loc_libere { get; set; }

        [ForeignKey("Tur")]
        [Column("cod_tur")]
        public int cod_tur { get; set; }

        [ForeignKey("Punct")]
        [Column("cod_punct")]
        public int cod_punct { get; set; }

        public virtual Tur? Tur { get; set; }
        public virtual Punct_Plecare? Punct { get; set; }
    }
}
