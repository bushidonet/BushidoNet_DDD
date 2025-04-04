using Microsoft.AspNetCore.Http;
using NLog;
using System.Diagnostics;
using System.Threading.Tasks;
using NLog.Fluent;
using ILogger = Microsoft.Extensions.Logging.ILogger;

public class LoggingMiddleware
{

    private readonly RequestDelegate _next;
    private static readonly Logger Logger = LogManager.GetLogger("LoggingMiddleware");

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Inicia un cronómetro
        var stopwatch = Stopwatch.StartNew();

        // Guarda datos del request
        var path = context.Request.Path;
        var method = context.Request.Method;

        // Log de entrada
        Logger.Info($"Request started: Method={method}, Path={path}");

        await _next(context);

        // Detén el cronómetro y calcula la duración
        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

        // Guarda datos del response
        var statusCode = context.Response.StatusCode;

        // Log de salida con todos los datos
        Logger.Info($"Response completed: Method={method}, Path={path}, StatusCode={statusCode}, TimeTaken={elapsedMilliseconds} ms");
    }
}