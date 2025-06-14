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

builder.Services.ConfigureApplicationCookie(options =>
{
    // setari cookie-uri
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

builder.Services.Configure<SendGridOptions>(builder.Configuration.GetSection("SendGrid"));

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
    // The default HSTS value is 30 days.
    // You may want to change this for production scenarios,
    // see https://aka.ms/aspnetcore-hsts.
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
