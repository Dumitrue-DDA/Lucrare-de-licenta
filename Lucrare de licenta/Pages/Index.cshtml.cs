using Adventour.Data;
using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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

    // Pentru form-ul de cautare
    public List<Punct_Plecare> Puncte_Plecare { get; set; } = new List<Punct_Plecare>();
    public List<Destinatie> Destinatii { get; set; } = new List<Destinatie>();

    // Va putea fi modificata de angajatii departamentului de Marketing
    public List<int> Tari_recomandate { get; set; }
        = new List<int> { 
        // Initializare de test
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

    [BindProperty(SupportsGet = true)]
    public string? QueryDestinatii { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? QueryPlecare { get; set; }

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

        // Initializare pentru form
        Puncte_Plecare = await _context.puncte_plecare
            .OrderBy(pp => pp.cod_punct)
            .ToListAsync();
        Destinatii = await _context.destinatii
            .OrderBy(d => d.cod_tara)
            .Include("Tara")
            .ToListAsync();

        return Page();
    }
    public async Task<IActionResult> OnGetSuggestionsAsync(string query, string? source)
    {
        if (string.IsNullOrEmpty(query) || query.Length < 2)
        {
            return new JsonResult(new List<SearchSuggestion>());
        }

        _logger.LogInformation("Fetching suggestions for Query: {Query}, Source: {Source}", query, source);
        var suggestions = await GetSearchSuggestionsFromDb(query, source);

        return new JsonResult(suggestions, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
    public async Task<List<SearchSuggestion>> GetSearchSuggestionsFromDb(string query, string? source)
    {
        const int maxSuggestions = 10;
        IQueryable<SearchSuggestion> queryableSuggestion;

        switch (source?.ToLowerInvariant())
        {
            case "destinatii":
                queryableSuggestion = _context.destinatii
                    .Where(d => EF.Functions.Like(d.den_destinatie, $"%{query}%"))
                    .Include("Tara")
                    .Select(d => new SearchSuggestion
                    {
                        Text = d.den_destinatie,
                        Optional1 = d.judet ?? null,
                        Optional2 = d.Tara.den_tara ?? null
                    });
                break;
            case "puncte_plecare":
                queryableSuggestion = _context.puncte_plecare
                    .Where(pp => EF.Functions.Like(pp.localitate, $"%{query}%"))
                    .Select(pp => new SearchSuggestion
                    {
                        Text = pp.localitate,
                        Optional1 = pp.judet ?? null,
                        Optional2 = null,
                    });
                break;
            default:
                return new List<SearchSuggestion>();
        }
        return await queryableSuggestion.Take(maxSuggestions).ToListAsync();
    }
}

public class SearchSuggestion
{
    // Textul principal (numele localitatii destinatie / plecare
    public string Text { get; set; } = string.Empty;

    // Subtextul (1 = judet/tara)
    public string? Optional1 { get; set; } = null;

    // (2 = Continent)
    public string? Optional2 { get; set; } = null;
}