using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventService.Data;
using EventService.Models;

namespace EventService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly EventsDbContext _db;
        public EventsController(EventsDbContext db) => _db = db;

        // GET /api/events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> Get()
        {
            var list = await _db.Events.ToListAsync();
            return Ok(list);
        }

        // GET /api/events/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Event>> GetOne(int id)
        {
            var model = await _db.Events.FindAsync(id);
            if (model == null) return NotFound();
            return Ok(model);
        }

        // POST /api/events
        [HttpPost]
        public async Task<ActionResult<Event>> Post([FromBody] Event eventModel)
        {
            if (eventModel == null)
                return BadRequest("Request body is missing");

            _db.Events.Add(eventModel);
            await _db.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetOne),
                new { id = eventModel.Id },
                eventModel
            );
        }

        // PUT /api/events/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] Event eventModel)
        {
            if (eventModel == null || id != eventModel.Id)
                return BadRequest("Invalid payload or mismatched id");

            var exists = await _db.Events.AnyAsync(e => e.Id == id);
            if (!exists) return NotFound();

            _db.Entry(eventModel).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /api/events/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _db.Events.FindAsync(id);
            if (model == null) return NotFound();

            _db.Events.Remove(model);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
