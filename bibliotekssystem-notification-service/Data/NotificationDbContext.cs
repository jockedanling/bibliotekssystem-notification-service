using bibliotekssystem_notification_service.Models;
using Microsoft.EntityFrameworkCore;

namespace bibliotekssystem_notification_service.Data;

public class NotificationDbContext : DbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
    {}
    
    //Varje DbSet = en tabell i databasen
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<NotificationTemplate> NotificationTemplates => Set<NotificationTemplate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Konfigruera relationen
        // En NotificationTemplate har många Notifications
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Template) // Notification har en template
            .WithMany(t => t.Notifications) // Template har många notificationer
            .HasForeignKey(n => n.TemplateId) // Via TemplateId-kolumnen
            .OnDelete(DeleteBehavior.Restrict); // Förhindra radering av mall som används
        
        // Index för snabbare sökningar
        modelBuilder.Entity<Notification>().HasIndex(n => n.UserId);
        modelBuilder.Entity<NotificationTemplate>().HasIndex(t => t.Type).IsUnique();
        
        // Seed-data: Fördefinierade mallar
        modelBuilder.Entity<NotificationTemplate>().HasData(new NotificationTemplate
            {
                Id = 1,
                Type = "LoanDueReminder",
                Subject = "Påminnelse: Lån förfaller snart",
                BodyTemplate = "Hej! Din lånade bok ska lämnas " + " tillbaka senast {DueDate}."
            },
            new NotificationTemplate
            {
                Id = 2,
                Type = "OverdueNotice",
                Subject = "Försenad återlämning",
                BodyTemplate = "Din lånade bok skulle ha lämnats " + " tillbaka {DueDate}. Vänligen återlämna snarast."
            },
            new NotificationTemplate
            {
                Id = 3,
                Type = "ReservationReady",
                Subject = "Din reservation är redo",
                BodyTemplate = "Boken du reserverade är nu " + " tillgänglig för upphämtning."
            },
            new NotificationTemplate
            {
                Id = 4,
                Type = "LoanConfirmation",
                Subject = "Lån bekräftat",
                BodyTemplate = "Du har lånat en bok. " + " Återlämningsdatum: {DueDate}."
            },
            new NotificationTemplate
            {
                Id = 5,
                Type = "ReturnConfirmation",
                Subject = "Återlämning bekräftad",
                BodyTemplate = "Tack! Din återlämning " + " har registrerats."
            });
    }
}