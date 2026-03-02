namespace bibliotekssystem_notification_service.Models;

public class Notification
{
    public int Id { get; set; }
    
    // Referensen till användare i UserService
    public int UserId { get; set; }
    
    // Främmande nyckel till NotificationTemplate
    public int TemplateId { get; set; }
    
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation property - Kopplar till mallens data. null! innebär att template sätts av Ef Core, och inte av koden. 
    public NotificationTemplate Template { get; set; } = null!;
}