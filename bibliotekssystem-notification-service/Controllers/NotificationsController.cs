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
}