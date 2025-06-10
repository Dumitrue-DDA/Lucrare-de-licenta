namespace Lucrare_de_licenta.ViewModels
{
    public class TurCardViewModel
    {
        public int cod_tur { get; set; }
        public string? den_tur { get; set; }
        public string? desc_tur { get; set; }
        public string? img_tur { get; set; }
        public int? tip_transport { get; set; }
        public byte? loc_libere { get; set; }
        public int zile { get; set; }
        public decimal? pret_min { get; set; }
        public List<string> Categorii { get; set; } = new List<string>();
        public List<string> Destinatii { get; set; } = new List<string>();
        public List<string> Tari { get; set; } = new List<string>();
    }
}
