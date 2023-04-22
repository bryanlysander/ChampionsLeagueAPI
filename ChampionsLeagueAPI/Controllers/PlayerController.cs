using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChampionsLeagueAPI.Models;
using Azure;
using System.Numerics;

namespace ChampionsLeagueAPI.Controllers
{
    [Route("championsleagueapi/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ChampionsLeagueDBContext _context;

        public PlayerController(ChampionsLeagueDBContext context)
        {
            _context = context;
        }

        // GET: championsleagueapi/Player
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            var players = _context.Players
            .Where(p => p.ClubId != 0)
            .Join(_context.Clubs, p => p.ClubId, c => c.ClubId, (p, c) => new Player
            {
                PlayerId = p.PlayerId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                JerseyNumber = p.JerseyNumber,
                Position = p.Position,
                Goals = p.Goals,
                ClubId = p.ClubId
            }).ToList();

            if (Ok(players) == null)
            {
                return NotFound();
            }

            var response = new ChampionsLeagueAPI.Models.Response<Player>
            {
                StatusCode = (players.Count > 0) ? StatusCodes.Status200OK : StatusCodes.Status204NoContent,
                StatusDescription = (players.Count > 0) ? "Players found" : "No players found",
                Items = players
            };

            return Ok(response);
        }

        // GET: championsleagueapi/Player/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var players = await _context.Players.FindAsync(id);

            var response = new ChampionsLeagueAPI.Models.Response<Player>
            {
                StatusCode = (players != null) ? StatusCodes.Status200OK : StatusCodes.Status204NoContent,
                StatusDescription = (players != null) ? "Players found" : "No players found",
                Items = new List<Player>() { players }
            };

            return Ok(response);
        }

        // PUT: championsleagueapi/Player/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.PlayerId)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // POST: championsleagueapi/Player
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            if (_context.Players == null)
            {
                return Problem("Entity set 'ChampionsLeagueDBContext.Players' is null.");
            }

            var newPlayer = new Player
            {
                FirstName = player.FirstName,
                LastName = player.LastName,
                JerseyNumber = player.JerseyNumber,
                Position = player.Position,
                Goals = player.Goals,
                ClubId = player.ClubId
            };

            var clubIds = _context.Clubs.Select(c => c.ClubId).ToList();

            if (!clubIds.Contains(newPlayer.ClubId))
            {
                return BadRequest("Invalid ClubId specified.");
            }

            _context.Players.Add(newPlayer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayer", new { id = newPlayer.PlayerId }, newPlayer);
        }

        // DELETE: championsleagueapi/Player/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            if (_context.Players == null)
            {
                return NotFound();
            }
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerExists(int id)
        {
            return (_context.Players?.Any(e => e.PlayerId == id)).GetValueOrDefault();
        }
    }
}
