using MovieApp.Middlewares;
using MovieApp.Models.Settings;
var builder = WebApplication.CreateBuilder(args);
// CreateBuilder configure déjà : Kestrel, appsettings*.json, variables d'environnement, journalisation console.
// ===== 1. SERVICES (ancien Startup.ConfigureServices) =====
builder.Services.AddControllersWithViews(); // active MVC : contrôleurs + vues Razor
builder.Services.Configure<MovieAppSettings>(
    builder.Configuration.GetSection("MovieAppSettings")
);
var app = builder.Build();
var nomApp = builder.Configuration["MovieAppSettings:NomApplication"];
Console.WriteLine($"Démarrage de {nomApp}");
// ===== 2. PIPELINE (ancien Startup.Configure) — L'ORDRE COMPTE =====
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // en production : page d'erreurpropre
    app.UseHsts();
}
app.UseHttpsRedirection(); // http -> https
app.UseStaticFiles(); // sert wwwroot et court-circuite pour un .css/.js trouvé
app.UseMiddleware<RequestLoggerMiddleware>(); // notre middleware (2.2)
app.UseRouting();
app.UseAuthorization();
// choisit l'endpoint (contrôleur/action)
// vérifie les droits sur l'endpoint choisi
// Route conventionnelle par défaut
app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run(); // démarre le serveur
