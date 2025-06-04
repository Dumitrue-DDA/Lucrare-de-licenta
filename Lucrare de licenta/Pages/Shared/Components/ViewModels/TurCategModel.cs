using Lucrare_de_licenta.Models;

namespace Lucrare_de_licenta.Pages.Shared.Components.ViewModels
{
    public class TurCategModel
    {
        public IEnumerable<Tur> Tururi { get; set; }
        public IEnumerable<Categorie> Categorii { get; set; }
    }
}
