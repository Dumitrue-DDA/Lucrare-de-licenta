using Adventour.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Lucrare_de_licenta.Pages.Admin
{
    public class DbAddModel : PageModel
    {
        private readonly ILogger<DbAddModel> _logger;
        private readonly AppDbContext _db;

        [BindProperty(SupportsGet = true)]
        public string Entity { get; set; } = string.Empty;
        public List<PropertyInfo> Props { get; private set; } = new List<PropertyInfo>();

        [BindProperty]
        public Dictionary<string, string>? Values { get; private set; }

        public string? ErrorMessage { get; set; } = null;
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
        public DbAddModel(ILogger<DbAddModel> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public List<string?> GetAllEntityNames()
        {
            return _db.Model.GetEntityTypes()
                      .Select(e => e.GetTableName())
                      .Where(tName => !string.IsNullOrEmpty(tName))
                      .OrderBy(name => name)
                      .ToList();
        }
        private Type? FindEntityType(string name)
        {
            return _db.Model.GetEntityTypes()
                .FirstOrDefault(e => e.GetTableName() == name)
                ?.ClrType;
        }

        public void OnGet()
        {
            try
            {
                var t = FindEntityType(Entity);
                if (t == null) throw new Exception($"Entity '{Entity}' not found.");
                Props = t.GetProperties()
                    .Where(p => p.CanWrite && p.GetSetMethod(false) != null)
                    .Where(p => Type.GetTypeCode(p.PropertyType) != TypeCode.Object)
                    .ToList();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Eroare: {ex}";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var t = FindEntityType(Entity);
                if (t == null) return NotFound();

                var entity = Activator.CreateInstance(t);

                foreach (var val in Values)
                {
                    var prop = t.GetProperty(val.Key);
                    if (prop == null) continue;

                    var typedVal = Convert.ChangeType(val.Value, prop.PropertyType);
                    prop.SetValue(entity, typedVal);
                }

                var setMethod = _db.GetType().GetMethod("Set");
                if (setMethod == null) throw new InvalidOperationException("Unable to find 'Set' method on DbContext.");

                var set = setMethod.MakeGenericMethod(t).Invoke(_db, null);
                if (set == null) throw new InvalidOperationException("Unable to invoke 'Set' method on DbContext.");

                var addMethod = set.GetType().GetMethod("Add");
                if (addMethod == null) throw new InvalidOperationException("Unable to find 'Add' method on DbSet.");

                addMethod.Invoke(set, new[] { entity });

                await _db.SaveChangesAsync();

                return RedirectToPage("./DbAdd", new { Entity });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding entity to database.");
                ModelState.AddModelError(string.Empty, "An error occurred while adding the entity. Please try again.");
                OnGet();
            }
            return Page();
        }
    }
}
