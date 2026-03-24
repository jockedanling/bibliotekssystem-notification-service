namespace bibliotekssystem_notification_service.Models;

// Återanvändbar mall för notifieringar.
public class NotificationTemplate
{
    public int Id { get; set; }
    
    //t.ex. "LoanDueReminder", "OverdueNotice", "LoanConfirmation"
    public string Type { get; set; } = string.Empty;
    
    public string Subject { get; set; } = string.Empty;
    
    //Mall med platshållare, t.ex. "Din bok ska lämnas tillbaka {DueDate}"
    public string BodyTemplate { get; set; } = string.Empty;
    
    //Navigation property - en mall kan ha många notifieringar
    public ICollection<Notification>  Notifications { get; set; } = new List<Notification>();
}