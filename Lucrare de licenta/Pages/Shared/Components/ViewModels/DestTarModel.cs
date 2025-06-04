using Lucrare_de_licenta.Models;

namespace Lucrare_de_licenta.Pages.Shared.Components.ViewModels
{
    public class DestTarModel
    {
        public IEnumerable<Destinatie> Destinatii { get; set; }
        public IEnumerable<Tara> Tari { get; set; }
    }
}
