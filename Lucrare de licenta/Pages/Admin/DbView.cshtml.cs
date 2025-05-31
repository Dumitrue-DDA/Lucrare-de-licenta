using Adventour.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Reflection;

namespace Lucrare_de_licenta.Pages.Admin
{
    public class DbViewModel : PageModel
    {
        private readonly ILogger<DbAddModel> _logger;
        private readonly AppDbContext _db;
        public DbViewModel(ILogger<DbAddModel> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [BindProperty(SupportsGet = true)]
        public string Entity { get; set; } = string.Empty;
        public List<PropertyInfo> Props { get; private set; } = new List<PropertyInfo>();

        public string? ErrorMessage { get; set; }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
        public List<object> Entries { get; private set; } = new List<object>();

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
        public async Task<List<object>> GetEntriesAsync(string tableName)
        {
            var dbSetProperty = _db.GetType()
                .GetProperties()
                .FirstOrDefault(p => p.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));

            if (dbSetProperty == null)
                throw new ArgumentException($"Table '{tableName}' not found.");

            var dbSet = dbSetProperty.GetValue(_db);
            var entityType = dbSetProperty.PropertyType.GenericTypeArguments[0];

            var toListAsyncMethod = typeof(EntityFrameworkQueryableExtensions)
                .GetMethods()
                .FirstOrDefault(m => m.Name == "ToListAsync" &&
                                     m.GetParameters().Length == 2 &&
                                     m.GetParameters()[0].ParameterType.IsGenericType &&
                                     m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IQueryable<>));

            var task = (Task)toListAsyncMethod.Invoke(null, new object[] { dbSet, CancellationToken.None });
            await task.ConfigureAwait(false);

            var resultProperty = task.GetType().GetProperty("Result");
            var result = (IList)resultProperty.GetValue(task);

            return result.Cast<object>().ToList();
        }

        public void OnGet()
        {
            try
            {
                var entityType = FindEntityType(Entity);
                if (entityType == null) throw new Exception($"Entity '{Entity}' not found.");

                // Get the properties of the entity type
                Props = entityType.GetProperties()
                    .Where(p => p.CanRead && !p.Name.Contains("BackingField"))
                    .ToList();

                // Get the entries
                Entries = GetEntriesAsync(Entity).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Eroare: {ex}";
            }
        }
    }
}
