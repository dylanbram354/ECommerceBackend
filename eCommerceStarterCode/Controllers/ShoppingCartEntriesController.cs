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
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost, Authorize]
        public IActionResult PostNewEntry([FromBody] ShoppingCartEntry value)
        {
            var userId = User.FindFirstValue("id");
            value.UserId = userId;
            _context.ShoppingCartEntries.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }
        [HttpDelete("delete/gameId_{gameId}"), Authorize]
        public IActionResult DeleteEntry(int gameId)
        {
            var userId = User.FindFirstValue("id");
            var entry = _context.ShoppingCartEntries.Where(r => (r.UserId == userId && r.GameId == gameId)).SingleOrDefault();
            if (entry != null)
            {
                _context.Remove(entry);
                _context.SaveChanges();
                return StatusCode(204);
            }
            else
            {
                return StatusCode(404);
            }
        }

        [HttpPut("edit/gameId_{gameId}"), Authorize]
        public IActionResult EditEntry(int gameId, [FromBody] ShoppingCartEntry newEntryBody) //body is JUST an int for new quantity
        {
            var userId = User.FindFirstValue("id");
            var entry = _context.ShoppingCartEntries.Where(r => (r.UserId == userId && r.GameId == gameId)).SingleOrDefault();
            if (entry != null)
            {
                _context.Remove(entry);
                ShoppingCartEntry newEntry = new ShoppingCartEntry { GameId = gameId, UserId = userId, Quantity = newEntryBody.Quantity };
                _context.Add(newEntry);
                _context.SaveChanges();
                return Ok(newEntry);
            }
            else
            {
                return StatusCode(404);
            }
        }

        [HttpGet(), Authorize]
        public IActionResult GetMyCart()
        {
            var userId = User.FindFirstValue("id");
            var entries = _context.ShoppingCartEntries.Include(sp => sp.Game).ThenInclude(spg => spg.User).Where(sp => sp.UserId == userId).ToList().
                Select(e => new { gameTitle = e.Game.Name, gameDescription = e.Game.Description, gamePrice = e.Game.Price, userId = e.UserId, gameId = e.GameId, seller = e.Game.User.UserName, sellerId = e.Game.User.Id, quantity = e.Quantity });
            if (entries != null)
            {
                return Ok(entries);
            }
            else
            {
                return NotFound();
            }
        }
    }
}