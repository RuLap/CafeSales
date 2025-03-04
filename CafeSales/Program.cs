using CafeSales.Data;
using CafeSales.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting a web application");
    
    var builder = WebApplication.CreateBuilder(args);
    
    builder.Host.ConfigureSerilog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONN_STRING");
    builder.Services.AddDbContext<CafeDbContext>(options =>
        options.UseNpgsql(connectionString));

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.ConfigureExceptionHandler();
    
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, ex.Message);
}
finally
{
    Log.CloseAndFlush();
}