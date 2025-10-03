using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Bind to port 5000
builder.WebHost.UseUrls("http://0.0.0.0:5000");

var app = builder.Build();

// Create a Gauge metric
var gauge = Metrics.CreateGauge("my_custom_metric", "Example of a custom time series metric");

// Start a background thread to update the metric every second
var timer = new Timer(_ =>
{
    // For example, generate a random value or use some logic
    var value = new Random().NextDouble() * 100;
    gauge.Set(value);
}, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

// Enable default HTTP metrics and routing
app.UseRouting();
app.UseHttpMetrics();

app.UseEndpoints(endpoints =>
{
    endpoints.MapMetrics();
});

app.MapGet("/", () => "Hello World!");

app.Run();
