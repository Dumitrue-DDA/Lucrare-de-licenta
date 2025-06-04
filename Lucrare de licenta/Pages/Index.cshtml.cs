using Adventour.Data;
using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lucrare_de_licenta.Pages;

public class IndexModel : PageModel
{
    public List<Tara> Tari { get; set; } = new List<Tara>();

    // Pentru afisare mai facila a continentelor 
    public Dictionary<byte, string> Continente = new Dictionary<byte, string> {
        { 1, "Europa" },
        { 2, "Africa" },
        { 3, "Asia" },
        { 4, "America de Nord" },
        { 5, "America de Sud" }
    };
    public List<Punct_Plecare> Puncte_Plecare { get; set; } = new List<Punct_Plecare>();

    // Va putea fi modificata de angajatii departamentului de Marketing
    public List<int> Tari_recomandate { get; set; }
        = new List<int> { // Initializare de test
        1,2,3,4,5,6,7,8
    };

    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext _context;
    public string Imagepath = "\\Resources\\";

    public IndexModel(AppDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task OnGetAutocompletePunctePlecare(string term)
    {
        List<Punct_Plecare> puncte = await _context.puncte_plecare
            .Where(pp => pp.localitate.Contains(term))
            .OrderBy(pp => pp.localitate)
            .Take(10)
            .ToListAsync();
        Puncte_Plecare = puncte;
    }
    public async Task<IActionResult> OnGetAsync()
    {
        if (_context == null)
        {
            _logger.LogError("Contextul este null.");
            return Page();
        }

        // Initializam elementele ce vor fi recomandate
        Tari = await _context.tari
            .Where(t => Tari_recomandate.Contains(t.cod_tara))
            .OrderBy(t => t.cod_tara)
            .ToListAsync();
        Puncte_Plecare = await _context.puncte_plecare
            .OrderBy(pp => pp.cod_punct)
            .ToListAsync();
        return Page();
    }
}
