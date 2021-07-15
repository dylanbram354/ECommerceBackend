using eCommerceStarterCode.Data;
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
    [Route("api/examples")]
    [ApiController]
    public class ExamplesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ExamplesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // <baseurl>/api/examples/user
        [HttpGet("user"), Authorize]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirstValue("id");
            var user = _context.Users.Find(userId);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //testing foreign key relationships in ShoppingCartEntries
        [HttpGet("userFromCart"), Authorize]

        public IActionResult GetUserEmailFromCartEntry()
        {
            var userEmail = _context.ShoppingCartEntries.Include(sc => sc.User).Where(sc=> sc.UserId == sc.User.Id).Select(sc => sc.User.Email);
            return Ok(userEmail);
        }

        [HttpGet("gameFromReview")]

        public IActionResult GetGameNameFromReview()
        {
            var gameName = _context.Reviews.Include(r => r.Game).Where(r => r.GameId == r.Game.GameId).Select(r => r.Game.Name);
            return Ok(gameName);
        }

        [HttpGet("userFromGame")]

        public IActionResult GetUserEmailFromGame()
        {
            var userEmail = _context.Games.Include(g => g.User).Where(g => g.UserId == g.User.Id).Select(g => g.User.Email);
            return Ok(userEmail);
        }
    }
}
