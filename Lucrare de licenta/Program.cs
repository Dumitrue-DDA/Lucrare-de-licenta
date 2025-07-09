using Adventour.Data;
using Lucrare_de_licenta.Models;
using Lucrare_de_licenta.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddRazorPages();

builder.Services.AddHttpClient();

// setari cookie-uri
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Lax;

    // pagini
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";

    // expirare
    options.ExpireTimeSpan = TimeSpan.FromHours(24);
    options.SlidingExpiration = true;
});

builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeAreaFolder("Identity", "/Identity/Account/Manage");
        options.Conventions.AuthorizeAreaPage("Identity", "/Identity/Account/Logout");
    });

// setari Autentificare 
builder.Services.AddIdentity<Utilizator, IdentityRole<int>>(options =>
{
    // Setari parola
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Setari blocare cont
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // !! Optiuni pentru dezvoltare, de schimbat in productie !!
    options.User.RequireUniqueEmail = false;
    options.SignIn.RequireConfirmedAccount = false;

})
.AddEntityFrameworkStores<AppDbContext>()
.AddErrorDescriber<LocErrorDescriber>()
.AddDefaultTokenProviders();

// Serviciul sendgrid
builder.Services.Configure<SendGridOptions>(builder.Configuration.GetSection("SendGrid"));

// Serviciile de administrare a autentificarii si a utilizatorilor
builder.Services.AddScoped<SignInManager<Utilizator>>();
builder.Services.AddScoped<UserManager<Utilizator>>();

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EsteAngajat", policy =>
        policy.RequireAssertion(context =>
            context.User?.Identity?.IsAuthenticated == true &&
            context.User.Claims.Any(c => c.Type == ClaimTypes.Role)));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Adaugare roluri
app.Lifetime.ApplicationStarted.Register(async () =>
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

        // in ordinea codurilor
        string[] roleNames = {
            "admin", // Administrator Web
            "man_soft", // Manager Software
            "ing_soft", // Inginer Software
            "man_op", // Manager Operatiuni
            "spec_dez", // Specialist Dezvoltare servicii turistice
            "fin" // (departament) Finante
        };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<int>(roleName));
            }
        }
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Utilizator>>();

        // !SETARI DE TEST! \\
        // setarea automata a rolului de administrator pentru contul cu un mail dat
        var user = await userManager.FindByEmailAsync("despina.andrei2003@gmail.com");

        if (user != null && !await userManager.IsInRoleAsync(user, "Admin"))
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
);

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
