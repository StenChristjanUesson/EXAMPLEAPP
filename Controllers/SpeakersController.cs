using ITB2203Application.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

namespace ITB2203Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpeakersController : Controller
    {
        private readonly DataContext _context;

        public SpeakersController(DataContext context)
        {
            _context = context;
        }

        //Speaker Section

        [HttpGet]
        public ActionResult<IEnumerable<Speaker>> GetSpeakers(string? Name = null)
        {
            var query = _context.Speakers!.AsQueryable();

            if (Name != null)
                query = query.Where(x => x.Name != null && x.Name.ToUpper().Contains(Name.ToUpper()));

            return query.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<TextReader> GetSpeaker(int id)
        {
            var speaker = _context.Speakers!.Find(id);

            if (speaker == null)
            {
                return NotFound();
            }

            return Ok(speaker);
        }

        [HttpPut("{id}")]
        public IActionResult PutSpeaker(int id, Speaker speaker)
        {
            var dbSpeaker = _context.Speakers!.AsNoTracking().FirstOrDefault(x => x.Id == speaker.Id);
            if (id != speaker.Id || dbSpeaker == null)
            {
                return NotFound();
            }
            _context.Update(speaker);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Speaker> PostSpeaker(Speaker speaker)
        {
            var dbSpeaker = _context.Speakers!.Find(speaker.Id);
            if (dbSpeaker == null)
            {
                _context.Add(speaker);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetSpeaker), new { Id = speaker.Id }, speaker);
            }
            else
            {
                return Conflict();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSpeaker(int id)
        {
            var speaker = _context.Speakers!.Find(id);
            if (speaker == null)
            {
                return NotFound();
            }

            _context.Remove(speaker);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
