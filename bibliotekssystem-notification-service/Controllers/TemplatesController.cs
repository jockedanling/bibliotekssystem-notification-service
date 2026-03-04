using bibliotekssystem_notification_service.Data;
using bibliotekssystem_notification_service.Models;
using bibliotekssystem_notification_service.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace bibliotekssystem_notification_service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TemplatesController : ControllerBase
{
    private readonly NotificationDbContext _context;
    
    public  TemplatesController(NotificationDbContext context)
    {
       _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NotificationTemplate>>> GetAll()
    {
        return await _context.NotificationTemplates.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NotificationTemplate>> GetById(int id)
    {
        var template = await _context.NotificationTemplates.FindAsync(id);
        
        if(template is null) 
            return NotFound(new
        {
            error = $"Mall med id {id} hittades inte."
        });
        return template;
    }

    [HttpPost]
    public async Task<ActionResult<NotificationTemplate>> Create(CreateTemplateDto dto)
    {
        var template = new NotificationTemplate
        {
            Type = dto.Type,
            Subject = dto.Subject,
            BodyTemplate = dto.BodyTemplate
        };

        _context.NotificationTemplates.Add(template);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetById), new { id = template.Id }, template);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTemplateDto dto)
    {
        var template = await _context.NotificationTemplates.FindAsync(id);

        if (template is null)
            return NotFound(new
            {
                error = $"Mall med id {id} hittades inte."
            });
        
        template.Type = dto.Type;
        template.Subject = dto.Subject;
        template.BodyTemplate = dto.BodyTemplate;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    public async Task<IActionResult> Delete(int id)
    {
        var template = await _context.NotificationTemplates.FindAsync(id);
        if (template is null)
            return NotFound(new
            {
                error = $"Mall med id {id} hittades inte."
            });
        
        //Blockera radering om mallen används
        var hasNotifications = await _context.Notifications.AnyAsync(n => n.TemplateId == id);

        if (hasNotifications)
            return Conflict(new
            {
                error = "Kan inte ta bort mall som används."
            });
        
        _context.NotificationTemplates.Remove(template);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}