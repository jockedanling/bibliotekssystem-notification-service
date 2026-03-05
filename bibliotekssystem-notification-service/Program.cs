using bibliotekssystem_notification_service.Data;
using bibliotekssystem_notification_service.Middleware;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;


namespace bibliotekssystem_notification_service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Registrera Controllers
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            // Undvika oändliga loopar i JSON
            // (Template => Notifications => Template => ...)
            options.JsonSerializerOptions.ReferenceHandler =
                System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });
        
        
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        
        // Konfiguera DbContext för SQLite, skapa databasen.
        builder.Services.AddDbContext<NotificationDbContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        }); 
        
        // CORS - Tillåt anrop från MVC-klienten
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();

            });
        });
        
       
        var app = builder.Build();
        
        // Databas migrerar vid start.
        
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<NotificationDbContext>();
            dbContext.Database.Migrate();
        } 
        
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
        

        app.UseHttpsRedirection();
        
        // API-nyckel kontroll
        app.UseMiddleware<ApiKeyMiddleware>();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}