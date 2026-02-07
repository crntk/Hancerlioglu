using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Hancerlioglu.Middleware;
using Microsoft.VSDiagnostics;

[SimpleJob]
[CPUUsageDiagnoser]
public class PerformanceMonitoringMiddlewareBenchmark
{
    private PerformanceMonitoringMiddleware _middlewareFast;
    private PerformanceMonitoringMiddleware _middlewareSlow;
    private DefaultHttpContext _context;
    [GlobalSetup]
    public void Setup()
    {
        // Fast delegate (no work)
        RequestDelegate fastNext = ctx => Task.CompletedTask;
        _middlewareFast = new PerformanceMonitoringMiddleware(fastNext, NullLogger<PerformanceMonitoringMiddleware>.Instance);
        // Slow delegate (simulate slow request)
        RequestDelegate slowNext = async ctx => await Task.Delay(600);
        _middlewareSlow = new PerformanceMonitoringMiddleware(slowNext, NullLogger<PerformanceMonitoringMiddleware>.Instance);
        _context = new DefaultHttpContext();
        _context.Request.Method = "GET";
        _context.Request.Path = "/benchmark";
    }

    [Benchmark(Baseline = true)]
    public Task FastRequest_InvokeAsync()
    {
        return _middlewareFast.InvokeAsync(_context);
    }

    [Benchmark]
    public Task SlowRequest_InvokeAsync()
    {
        return _middlewareSlow.InvokeAsync(_context);
    }
}