
using ChatAI.Models.Database;
using ChatAI.Services;
using Core;
using Microsoft.ApplicationInsights;
using Microsoft.EntityFrameworkCore;

namespace ChatAI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Agregar IHttpClientFactory antes de construir la aplicación
            builder.Services.AddHttpClient();

            // Agregar controladores con vistas
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IOpenAiService,OpenAiService>();

            builder.Services.AddDbContext<AgenteAiContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("AgenteAIDatabase")));


            builder.Services.AddApplicationInsightsTelemetry();

            var app = builder.Build();

            var telemetryClient = app.Services.GetRequiredService<TelemetryClient>();
            LoggerService.Configure(telemetryClient);

            app.UseMiddleware<LoggingMiddleware>();
            app.UseMiddleware<IdTransaccionMiddleware>();

            // Configurar el pipeline de middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
