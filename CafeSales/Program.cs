using CafeSales.Data;
using CafeSales.Extensions;
using CafeSales.Repository;
using CafeSales.Services;
using CafeSales.Services.Interfaces;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting a web application");
    
    var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
    if (File.Exists(envPath))
    {
        Env.Load(envPath);
    }
    else
    {
        Log.Warning(".env file not found in {Path}", envPath);
    }
    
    var builder = WebApplication.CreateBuilder(args);
    
    builder.Host.ConfigureSerilog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    
    builder.Services.AddScoped<IProductService, ProductService>();

    var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONN_STRING");
    builder.Services.AddDbContext<CafeDbContext>(options =>
        options.UseNpgsql(connectionString));
    
    builder.Services.AddScoped<DbContext>(provider => 
        provider.GetRequiredService<CafeDbContext>());

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