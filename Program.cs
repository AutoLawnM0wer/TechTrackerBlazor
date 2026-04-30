using DotNetEnv;
using TechTrackerBlazor.Components;
using TechTrackerBlazor.Settings;
using TechTrackerBlazor.Services;

namespace TechTrackerBlazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotNetEnv.Env.Load("connection.env");

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.Configure<MongoDbSettings>(
            builder.Configuration.GetSection("MongoDbSettings"));
            
            builder.Services.AddSingleton<TechTrackerHell>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
