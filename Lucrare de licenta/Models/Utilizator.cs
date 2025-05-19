using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lucrare_de_licenta.Models
{
    [Table("utilizatori")]
    public class Utilizator : IdentityUser<int>
    {
    }
}
