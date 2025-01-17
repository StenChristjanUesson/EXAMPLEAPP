using ITB2203Application.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

namespace ITB2203Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendeesController : ControllerBase
{
    private readonly DataContext _context;

    public AttendeesController(DataContext context)
    {
        _context = context;
    }

    //Attendee Section

    [HttpGet]
    public ActionResult<IEnumerable<Attendee>> GetAttendees(string? Name = null)
    {
        var query = _context.Attendees!.AsQueryable();

        if (Name != null)
            query = query.Where(x => x.Name != null && x.Name.ToUpper().Contains(Name.ToUpper()));

        return query.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<TextReader> GetAttendee(int id)
    {
        var attendee = _context.Attendees!.Find(id);

        if (attendee == null)
        {
            return NotFound();
        }

        return Ok(attendee);
    }

    [HttpPut("{id}")]
    public IActionResult PutAttendee(int id, Attendee attendee)
    {
        var dbAttendee = _context.Attendees!.AsNoTracking().FirstOrDefault(x => x.Id == attendee.Id);
        if (id != attendee.Id || dbAttendee == null)
        {
            return NotFound();
        }

        _context.Update(attendee);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    public ActionResult<Attendee> PostAttendee(Attendee attendee)
    {
        var dbAttendee = _context.Attendees!.Find(attendee.Id);
        if (dbAttendee == null)
        {
            _context.Add(attendee);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAttendee), new { Id = attendee.Id }, attendee);
        }
        else
        {
            return Conflict();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAttendee(int id)
    {
        var attendee = _context.Attendees!.Find(id);
        if (attendee == null)
        {
            return NotFound();
        }

        _context.Remove(attendee);
        _context.SaveChanges();

        return NoContent();
    }    
}
