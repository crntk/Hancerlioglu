using Hancerlioglu.Services;
using Hancerlioglu.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register HttpClient for GooglePlacesService with timeout
builder.Services.AddHttpClient<IGooglePlacesService, GooglePlacesService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(10);
});

// Add Memory Cache for performance
builder.Services.AddMemoryCache();

// Add Response Caching
builder.Services.AddResponseCaching();

// Add Response Compression for faster load times
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.BrotliCompressionProvider>();
    options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.GzipCompressionProvider>();
});

builder.Services.Configure<Microsoft.AspNetCore.ResponseCompression.BrotliCompressionProviderOptions>(options =>
{
options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

builder.Services.Configure<Microsoft.AspNetCore.ResponseCompression.GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    // Development ortamýnda performance monitoring
    app.UsePerformanceMonitoring();
}

// Use Response Compression (should be early in pipeline)
app.UseResponseCompression();

app.UseHttpsRedirection();

// Configure Static Files with caching
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
    // Cache static files for 30 days
        const int durationInSeconds = 60 * 60 * 24 * 30;
      ctx.Context.Response.Headers.Append("Cache-Control", $"public,max-age={durationInSeconds}");
    }
});

app.UseRouting();

// Use Response Caching
app.UseResponseCaching();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=AnaSayfa}/{id?}");

app.Run();
