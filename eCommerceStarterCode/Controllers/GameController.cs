using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eCommerceStarterCode.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GameController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost, Authorize]

        public IActionResult PostNewGame([FromBody]Game value)
        {
            var userId = User.FindFirstValue("id");
            value.UserId = userId;
            _context.Games.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);

        }

        [HttpDelete, Authorize]
        public IActionResult DeleteGame([FromBody] int gameId)
        {
            var userId = User.FindFirstValue("id");
            var game = _context.Games.Where(g => g.GameId == gameId).SingleOrDefault();

            if (game.UserId == userId)
            {
                _context.Games.Remove(game);
                _context.SaveChanges();
                return StatusCode(204);
            }
            else
            {
                return StatusCode(401);
            }

        }

        [HttpGet("all")]

        public IActionResult GetAllGames()
        {
            var games = _context.Games.Include(g => g.Platform).Include(g => g.User).Select(g => new
            {
                gameId = g.GameId,
                name = g.Name,
                price = g.Price,
                description = g.Description,
                platformId = g.PlatformId,
                platform = g.Platform,
                userId = g.UserId,
                seller = g.User.UserName
            });
            return Ok(games);
        }

        [HttpGet("{platformName}/all")]

        public IActionResult GetGamesByPlatformName(string platformName)
        {
            var games = _context.Games.Include(g => g.Platform).Include(g => g.User).Where(g => g.Platform.Name == platformName).Select(g => new
            {
                gameId = g.GameId,
                name = g.Name,
                price = g.Price,
                description = g.Description,
                platformId = g.PlatformId,
                platform = g.Platform,
                userId = g.UserId,
                seller = g.User.UserName
            }); 
            return Ok(games);
        }

        [HttpGet("{gameId}")]

        public IActionResult GetGameById(int gameId)
        {
            var game = _context.Games.Include(g => g.Platform).Include(g => g.User).Where(g => g.GameId == gameId).Select(g => new
            {
                gameId = g.GameId,
                name = g.Name,
                price = g.Price,
                description = g.Description,
                platformId = g.PlatformId,
                platform = g.Platform,
                userId = g.UserId,
                seller = g.User.UserName
            }).SingleOrDefault(); 
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpGet("title={gameName}")]

        public IActionResult GetGameByName(string gameName)
        {
            var game = _context.Games.Include(g => g.Platform).Include(g => g.User).Where(g => g.Name.ToUpper() == gameName.ToUpper()).Select(g => new
            {
                gameId = g.GameId,
                name = g.Name,
                price = g.Price,
                description = g.Description,
                platformId = g.PlatformId,
                platform = g.Platform,
                userId = g.UserId,
                seller = g.User.UserName
            });
            if (game == null)
            {
                return NotFound(gameName);
            }
            else
            {
                return Ok(game);
            }
            
        }

        [HttpGet("platforms/all")]

        public IActionResult GetAllPlatforms()
        {
            var platforms = _context.Platforms;
            return Ok(platforms);
        }
    }
}
