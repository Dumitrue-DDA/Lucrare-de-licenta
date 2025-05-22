using Adventour.Data;
using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucrare_de_licenta.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext _context;
    public string Imagepath = "\\Resources";

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    public List<Tara> tarilist { get; set; }
    public void OnGet()
    {
        if (_context != null)
        {
            tarilist = _context.tari.ToList();
        }
    }
}
