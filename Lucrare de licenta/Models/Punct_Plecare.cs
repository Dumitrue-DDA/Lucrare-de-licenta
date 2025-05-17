using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("puncte_pecare")]
    public class Punct_Plecare
    {
        [Key]
        [Column("cod_punct", TypeName = "tinyint")]
        public byte cod_punct { get; set; }

        [Required]
        [Column("tip_transport", TypeName = "bit")]
        public bool tip_transport { get; set; }

        [Required]
        [StringLength(100)]
        [Column("localitate", TypeName = "nvarchar(100)")]
        public string localitate { get; set; }

        [StringLength(100)]
        [Column("judet", TypeName = "nvarchar(100)")]
        public string? judet { get; set; }

        [StringLength(200)]
        [Column("adresa", TypeName = "nvarchar(200)")]
        public string? adresa { get; set; }
    }
}
