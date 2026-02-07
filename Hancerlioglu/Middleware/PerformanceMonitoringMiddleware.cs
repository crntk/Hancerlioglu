using System.Diagnostics;

namespace Hancerlioglu.Middleware
{
    /// <summary>
    /// Request performance tracking middleware
    /// </summary>
    public class PerformanceMonitoringMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PerformanceMonitoringMiddleware> _logger;
        private const int SlowRequestThresholdMs = 500;

        public PerformanceMonitoringMiddleware(RequestDelegate next, ILogger<PerformanceMonitoringMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                await _next(context);
            }
            finally
            {
                sw.Stop();

                if (sw.ElapsedMilliseconds > SlowRequestThresholdMs)
                {
                    if (_logger.IsEnabled(LogLevel.Warning))
                    {
                        _logger.LogWarning(
                            "Slow request detected: {Method} {Path} took {ElapsedMs}ms",
                            context.Request.Method,
                            context.Request.Path,
                            sw.ElapsedMilliseconds);
                    }
                }
                else
                {
                    if (_logger.IsEnabled(LogLevel.Debug))
                    {
                        _logger.LogDebug(
                            "Request completed: {Method} {Path} in {ElapsedMs}ms",
                            context.Request.Method,
                            context.Request.Path,
                            sw.ElapsedMilliseconds);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Extension method for middleware registration
    /// </summary>
    public static class PerformanceMonitoringMiddlewareExtensions
    {
        public static IApplicationBuilder UsePerformanceMonitoring(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PerformanceMonitoringMiddleware>();
        }
    }
}
