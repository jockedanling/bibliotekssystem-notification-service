using bibliotekssystem_notification_service.Data;
using Microsoft.AspNetCore.Mvc;
using bibliotekssystem_notification_service.Models;
using bibliotekssystem_notification_service.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace bibliotekssystem_notification_service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly NotificationDbContext _context;

    public NotificationsController(NotificationDbContext context)
    {
        _context = context;
       
    }


// READ - Hämta data (GET)

// GET: api/notifications
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Notification>>> GetAll()
    {
        return await _context.Notifications
            .Include(n => n.Template)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }
    
    // GET: api/notifications/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Notification>> GetById(int id)
    {
        var notification = await _context.Notifications
            .Include(n => n.Template)
            .FirstOrDefaultAsync(n => n.Id == id);

        if (notification is null)
        {
            return NotFound(new
            {
                error = $"Notifiering med id {id} hittades inte."
            });
        }
        return notification;
    }
    
    //GET: api/notifications/user/3
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<Notification>>> GetByUser(int userId)
    {
        return await _context.Notifications
            .Include(n => n.Template)
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }
    
    //GET: api/notifications/user/3/unread
    [HttpGet("user/{userId}/unread")]
    public async Task<ActionResult<IEnumerable<Notification>>> GetUnreadByUser(int userId)
    {
        return await _context.Notifications
            .Include(n => n.Template)
            .Where(n => n.UserId == userId && !n.IsRead)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }
    
    
   //  CREATE - Skapa ny (POST)
   
   // POST: api/notifications
   [HttpPost]
   public async Task<ActionResult<Notification>> Create(CreateNotificationDto dto)
   {
       //Hämta mallen
       var template = await _context.NotificationTemplates.FindAsync(dto.TemplateId);

       if (template is null)
           return BadRequest(new
           {
               error = $"Mall med id {dto.TemplateId} finns inte."
           
           });
       
       //Skapa nytt objekt
       var notification = new Notification
       {
           UserId = dto.UserId,
           TemplateId = dto.TemplateId,
           Title = dto.CustomTitle ?? template.Subject,
           Message = dto.CustomMessage ?? template.BodyTemplate,
           IsRead = false,
           CreatedAt = DateTime.Now
       };
       
       //Lägg till i databasen
       _context.Notifications.Add(notification);
       await _context.SaveChangesAsync();
       
       //Ladda template för response
       await _context.Entry(notification)
           .Reference(n => n.Template)
           .LoadAsync();
       
       return CreatedAtAction(nameof(GetById), new { id = notification.Id }, notification);
   }
   
   // UPDATE - uppdatera (PUT)
   // PUT: api/notifications/5/read
   [HttpPut("{id}/read")]
   public async Task<IActionResult> MarkAsRead(int id)
   {
       var notification = await _context.Notifications.FindAsync(id);
       if (notification is null)
           return NotFound(new
           {
            error = $"Notifiering med id {id} hittades inte."
           });

       notification.IsRead = true;
       await _context.SaveChangesAsync();
       return NoContent();
   }
   
   // PUT: api/notifications/5
   [HttpPut("{id}")]
   public async Task<IActionResult> Update(int id, UpdateNotificationDto dto)
   {
       var notification = await _context.Notifications.FindAsync(id);
       if (notification is null)
           return NotFound(new
           {
               error = $"Notifiering med id {id} hittades inte."
           });
       
       notification.IsRead = dto.IsRead;
       await _context.SaveChangesAsync();
       return NoContent();
   }
   
   // DELETE - Ta bort
   
   //DELETE: api/notifications/5
   [HttpDelete("{id}")]
   public async Task<IActionResult> Delete(int id)
   {
       var notification = await _context.Notifications.FindAsync(id);
       if (notification is null)
           return NotFound(new
           {
               error = $"Notifiering med id {id} hittades inte."
           });

       _context.Notifications.Remove(notification);
       await _context.SaveChangesAsync();
       return NoContent();
   }
}