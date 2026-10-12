
using System.Diagnostics;
namespace MovieApp.Middlewares;
// Middleware "classe" : un constructeur qui reçoit RequestDelegate
// et une méthode InvokeAsync(HttpContext).
public class RequestLoggerMiddleware
{
    private readonly RequestDelegate _next;
    public RequestLoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var chrono = Stopwatch.StartNew();
        Console.WriteLine($"--> {context.Request.Method} {context.Request.Path}");
        await _next(context); // on passe la main au middleware suivant
        chrono.Stop();
        Console.WriteLine($"<-- {context.Response.StatusCode} en  {chrono.ElapsedMilliseconds} ms");
    }
}