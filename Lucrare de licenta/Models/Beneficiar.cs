using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("beneficiari")]
    public class Beneficiar
    {
        [Key]
        [Column("cod_beneficiar")]
        public int cod_beneficiar { get; set; }

        [Required]
        [Column("nume_beneficiar", TypeName = "nvarchar(50)")]
        public string nume_beneficiar { get; set; }

        [Required]
        [Column("prenume_beneficiar", TypeName = "nvarchar(50)")]
        public string prenume_beneficiar { get; set; }

        [Required]
        [Column("data_nastere", TypeName = "date")]
        public DateOnly data_nastere { get; set; }

        [Required]
        [ForeignKey("Camera")]
        [Column("cod_camera")]
        public int cod_camera { get; set; }

        public virtual Camera? Camera { get; set; }
    }
}
