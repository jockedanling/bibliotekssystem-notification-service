namespace bibliotekssystem_notification_service.Models.Dto;

public class CreateNotificationDto
{
    public int UserId { get; set; }
    public int TemplateId { get; set; }
    public string? CustomTitle { get; set; }
    public string? CustomMessage { get; set; }
}