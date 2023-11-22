using Microsoft.EntityFrameworkCore;
using RollDiceSimulation.Data;
using RollDiceSimulation.Services;

namespace RollDiceSimulation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        });

        //  Add services to the container
        builder.Services.AddHealthChecks();
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<RollDiceDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FuelPricingConnection")));
        builder.Services.AddScoped<IDiceRollerService, DiceRollerService>();
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/RollDice/Error");
        }
        
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapHealthChecks("/health");
        app.MapControllerRoute(name: "default", pattern: "{controller=RollDice}/{action=Index}/{id?}");
        app.Run();
    }
}