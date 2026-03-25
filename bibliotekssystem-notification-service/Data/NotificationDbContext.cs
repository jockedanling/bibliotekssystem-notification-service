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
    
    // Konfigurera relationen
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // En NotificationTemplate har många Notifications
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Template) // Notification har en template
            .WithMany(t => t.Notifications) // Template har många notifikationer
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
                BodyTemplate = "Hej! Ditt lånade material ska lämnas tillbaka senast {DueDate}."
            },
            new NotificationTemplate
            {
                Id = 2,
                Type = "OverdueNotice",
                Subject = "Försenad återlämning",
                BodyTemplate = "Ditt lånade material skulle ha lämnats tillbaka {DueDate}. Vänligen återlämna snarast."
            },
            new NotificationTemplate
            {
                Id = 3,
                Type = "ReservationReady",
                Subject = "Din reservation är redo",
                BodyTemplate = "Det du reserverade är nu tillgängligt för upphämtning."
            },
            new NotificationTemplate
            {
                Id = 4,
                Type = "LoanConfirmation",
                Subject = "Lån bekräftat",
                BodyTemplate = "Du har lånat ett objekt. Återlämningsdatum: {DueDate}."
            },
            new NotificationTemplate
            {
                Id = 5,
                Type = "ReturnConfirmation",
                Subject = "Återlämning bekräftad",
                BodyTemplate = "Tack! Din återlämning har registrerats."
            },
            new NotificationTemplate
            {
                Id = 6,
                Type = "AdminCatalogChange",
                Subject ="Ändring i hantera katalog",
                BodyTemplate = "En förändring har gjorts i hantera katalog."
            },
            new NotificationTemplate
            {
                Id = 7,
                Type = "AdminUserChange",
                Subject = "Ändring i hantera användare",
                BodyTemplate = "En förändring har gjorts i hantera användare."
            },
            new NotificationTemplate
            {
                Id = 8,
                Type = "AdminReviewChange",
                Subject = "Ändring i hantera recensioner",
                BodyTemplate = "En förändring har gjorts i hantera recensioner."
            }
            );
    }
}