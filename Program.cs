using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using sunobra.Model;   // Tu DbContext de negocio (ya existe)
using sunobra.Data;    // NUEVO: namespace donde crear�s ApplicationUser y ApplicationDbContext

var builder = WebApplication.CreateBuilder(args);

// 1) MVC + Razor Pages (Identity usa Razor Pages)
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// 2) Cadena de conexi�n (ya existente)
var conString = builder.Configuration.GetConnectionString("conexion") ??
    throw new InvalidOperationException("Connection string 'conexion' not found.");

// 3) DbContext del CRUD (ya existente)
builder.Services.AddDbContext<SunobraDbContext>(options =>
    options.UseMySql(conString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb")));

// 4) DbContext de Identity (NUEVO)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(conString, ServerVersion.AutoDetect(conString)));

// 5) Identity (NUEVO)
builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        // Reglas de contrase�a relajadas para desarrollo:
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();   // <- Debe ir antes de UseAuthorization
app.UseAuthorization();

// P�ginas de Identity (Login/Registro/Logout)
app.MapRazorPages();

// Ruta MVC por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
