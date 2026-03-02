namespace bibliotekssystem_notification_service.Models.Dto;

public class CreateTemplateDto
{
    public string Type { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string BodyTemplate { get; set; } = string.Empty;
}