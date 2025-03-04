using Serilog;

namespace CafeSales.Extensions;

public static class SerilogExtensions
{
    public static void ConfigureSerilog(this IHostBuilder host)
    {
        var seqUrl = Environment.GetEnvironmentVariable("SEQ_URL");
        host.UseSerilog((ctx, lc) =>
        {
            lc.WriteTo.Console();
        });
    }
}