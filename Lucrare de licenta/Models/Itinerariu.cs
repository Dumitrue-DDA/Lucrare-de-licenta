using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("itinerarii")]
    public class Itinerariu
    {
        [Key]
        [Column("cod_itinerariu")]
        public int cod_itinerariu { get; set; }

        [StringLength(100)]
        [Column("titlu_itin", TypeName = "nvarchar(100)")]
        public string? titlu_itin { get; set; }

        [StringLength(500)]
        [Column("desc_itin", TypeName = "nvarchar(500)")]
        public string? desc_itin { get; set; }

        [StringLength(250)]
        [Column("img_itin", TypeName = "nvarchar(250)")]
        public string? img_itin { get; set; }

        [Column("zi_activitate", TypeName = "tinyint")]
        public byte zi_activitate { get; set; }

        [ForeignKey("Tur")]
        [Column("cod_tur")]
        public int cod_tur { get; set; }

        [ForeignKey("Cazare")]
        [Column("cod_cazare")]
        public int? cod_cazare { get; set; } = null;

        public virtual Tur? Tur { get; set; }
        public virtual Cazare? Cazare { get; set; }
    }
}
