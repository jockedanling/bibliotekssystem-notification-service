using bibliotekssystem_notification_service.Data;
using Microsoft.EntityFrameworkCore;

namespace bibliotekssystem_notification_service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        
        // Konfiguera DbContext för SQLite, hur ska databasen sättas upp?
        /*
         builder.Services.AddDbContext<NotificationDbContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        }); 
        */
       
        var app = builder.Build();
        
        // Databas migrationer vid start, senare skede?
        
        /*using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<NotificationDbContext>();
            dbContext.Database.Migrate();
        } */
        
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}