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

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeAreaFolder("Identity", "/Identity/Account/Manage");
        options.Conventions.AuthorizeAreaPage("Identity", "/Identity/Account/Logout");
    });

// Add this to enable the default UI
builder.Services.AddIdentity<Utilizator, IdentityRole<int>>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Optiuni pentru dezvoltare, de schimbat in productie
    options.User.RequireUniqueEmail = false;
    options.SignIn.RequireConfirmedAccount = false;

})
.AddEntityFrameworkStores<AppDbContext>()
.AddErrorDescriber<LocErrorDescriber>()
.AddDefaultTokenProviders();

// Make sure you have the SignInManager and UserManager injected properly
builder.Services.AddScoped<SignInManager<Utilizator>>();
builder.Services.AddScoped<UserManager<Utilizator>>();

builder.Services.AddSingleton<IEmailSender, NoOpEmailSender>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EsteAngajat", policy =>
        policy.RequireAssertion(context =>
            context.User?.Identity?.IsAuthenticated == true &&
            context.User.Claims.Any(c => c.Type == ClaimTypes.Role)));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

// Adaugam rolurile si atribuim administratorului web rolul sau
app.Lifetime.ApplicationStarted.Register(async () =>
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

        string[] roleNames = { "admin" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<int>(roleName));
            }
        }
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Utilizator>>();

        var user = await userManager.FindByEmailAsync("despina.andrei2003@gmail.com");

        if (user != null && !await userManager.IsInRoleAsync(user, "Admin"))
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
);

app.UseHttpsRedirection();
app.UseStaticFiles();

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
