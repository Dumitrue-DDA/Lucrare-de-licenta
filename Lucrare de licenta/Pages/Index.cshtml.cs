using Adventour.Data;
using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucrare_de_licenta.Pages;

public class IndexModel : PageModel
{
    public List<Tara> tarilist { get; set; }
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext _context;
    public string Imagepath = "\\Resources";

    public IndexModel(AppDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void OnGet()
    {
        if (_context != null)
        {
            tarilist = _context.tari.ToList();
        }
    }
}
