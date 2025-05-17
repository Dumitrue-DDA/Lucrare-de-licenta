using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("Camere")]
    public class Camera
    {
        [Key]
        [Column("cod_camera")]
        public int cod_camera { get; set; }

        [ForeignKey("Rezervare")]
        [Column("cod_rezervare")]
        public int cod_rezervare { get; set; }

        public virtual Rezervare? Rezervare { get; set; }
    }
}
