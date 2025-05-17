using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("destinatii_itinerarii")]
    public class Destinatie_itinerariu
    {
        [Key]
        [Column("cod_dest_itin")]
        public int cod_dest_itin { get; set; }

        [ForeignKey("Itinerariu")]
        [Column("cod_itinerariu")]
        public int cod_itinerariu { get; set; }

        [ForeignKey("Destinatie")]
        [Column("cod_destinatie", TypeName = "smallint")]
        public short cod_destinatie { get; set; }

        public virtual Itinerariu? Itinerariu { get; set; }
        public virtual Destinatie? Destinatie { get; set; }
    }
}
