using ExampleMasstransit.WebApi.Core;
using ExampleMasstransit.WebApi.Core.Extensions;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.AddSerilog("API MassTransit");
    Log.Information("Starting API");

    var appSettings = new AppSettings();
    builder.Configuration.Bind(appSettings);

    builder.Services.AddRouting(options => options.LowercaseUrls = true);

    builder.Services.AddControllers();
    builder.Services.AddOpenTelemetry(appSettings);

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new() { Title = "ExampleMasstransit.WebApi", Version = "v1" });
    });

    builder.Services.AddMasstransitExtension(builder.Configuration);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExampleMasstransitMasstransit.WebApi v1"));
    }
    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.MapControllers();

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}