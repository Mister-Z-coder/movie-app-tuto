
using System.Diagnostics;
using Microsoft.Extensions.Options;
using MovieApp.Models.Settings;
// Middleware "classe" : un constructeur qui reçoit RequestDelegate
// et une méthode InvokeAsync(HttpContext).
public class RequestLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggerMiddleware> _logger;
    private readonly int _seuilMs;

    // ILogger et IOptions sont fournis par l'injection de dépendances
    public RequestLoggerMiddleware(RequestDelegate next,
    ILogger<RequestLoggerMiddleware> logger,
    IOptions<MovieAppSettings> options)
    {
        _next = next;
        _logger = logger;
        _seuilMs = options.Value.SeuilRequeteLenteMs;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var chrono = Stopwatch.StartNew();
        await _next(context);
        chrono.Stop();
        var statut = context.Response.StatusCode;
        var duree = chrono.ElapsedMilliseconds;
        // Journalisation structurée : les {Noms} deviennent des champs exploitables
        if (statut >= 500)
            _logger.LogError("{Methode} {Chemin} -> {Statut} en {Duree} ms",
                context.Request.Method, context.Request.Path, statut, duree);
        else if (duree > _seuilMs)
            _logger.LogWarning("Requête lente : {Methode} {Chemin} en {Duree} ms", context.Request.Method, context.Request.Path, duree);
        else
            _logger.LogInformation("{Methode} {Chemin} -> {Statut} en {Duree} ms",context.Request.Method, context.Request.Path, statut, duree);
    }
}