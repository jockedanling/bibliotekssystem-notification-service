namespace bibliotekssystem_notification_service.Models.Dto;

// Vid PUT kan man bara ändra IsRead-status -  inte titel eller meddelande.
public class UpdateNotificationDto
{
    public bool IsRead { get; set; }
}