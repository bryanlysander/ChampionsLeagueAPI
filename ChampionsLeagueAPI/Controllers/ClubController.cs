using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChampionsLeagueAPI.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace ChampionsLeagueAPI.Controllers
{
    [Route("championsleagueapi/club")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly ChampionsLeagueDBContext _context;

        public ClubController(ChampionsLeagueDBContext context)
        {
            _context = context;
        }

        // GET: championsleagueapi/Club
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Club>>> GetClubs()
        {
            var clubs = await _context.Clubs.ToListAsync();


            if (clubs.Count == 0)
            {
                return NotFound();
            }

            var response = new ChampionsLeagueAPI.Models.Response<Club>
            {
                StatusCode = (clubs.Count > 0) ? StatusCodes.Status200OK : StatusCodes.Status204NoContent,
                StatusDescription = (clubs.Count > 0) ? "Clubs found" : "No clubs found",
                Items = clubs
            };

            return Ok(response);
        }

        // GET: championsleagueapi/Club/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Club>> GetClub(int id)
        {
            if (_context.Clubs == null)
            {
                return NotFound();
            }
            var club = await _context.Clubs.FindAsync(id);

            var response = new ChampionsLeagueAPI.Models.Response<Club>
            {
                StatusCode = (club != null) ? StatusCodes.Status200OK : StatusCodes.Status204NoContent,
                StatusDescription = (club != null) ? "club found" : "club not found",
                Items = new List<Club>() { club }
            };

            return Ok(response);
        }

        // PUT: championsleagueapi/Club/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClub(int id, Club club)
        {
            if (id != club.ClubId)
            {
                return BadRequest();
            }

            _context.Entry(club).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClubExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: championsleagueapi/Club
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Club>> PostClub(Club club)
        {
          if (_context.Clubs == null)
          {
              return Problem("Entity set 'ChampionsLeagueDBContext.Clubs'  is null.");
          }
            _context.Clubs.Add(club);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClub", new { id = club.ClubId }, club);
        }

        // DELETE: championsleagueapi/Club/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            if (_context.Clubs == null)
            {
                return NotFound();
            }
            var club = await _context.Clubs.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }

            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClubExists(int id)
        {
            return (_context.Clubs?.Any(e => e.ClubId == id)).GetValueOrDefault();
        }
    }
}
