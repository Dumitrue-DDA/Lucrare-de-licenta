using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("utilizatori")]
    public class Utilizator
    {
        [Key]
        [Column("nr_utilizator")]
        public int nr_utilizator { get; set; }

        [Required]
        [StringLength(160)]
        [Column("email", TypeName = "nvarchar(160)")]
        public string email { get; set; }

        [Required]
        [StringLength(255)]
        [Column("parola", TypeName = "nvarchar(255)")]
        public string parola { get; set; }

        [Range(0, 5)]
        [Column("tip_utilizator", TypeName = "tinyint")]
        public byte? tip_angajat { get; set; } = null;

        [Column("data_inregistrare", TypeName = "date")]
        public DateOnly data_inregistrare { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
