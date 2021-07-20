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
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("game_{gameId}")]

        public IActionResult GetReviewsByGameId(int gameId)
        {
            var reviews = _context.Reviews.Where(r => r.GameId == gameId).Include(r => r.User).Include(r => r.Game).
                Select(r => new { gameId = r.GameId, gameTitle = r.Game.Name, userId = r.UserId, rating = r.Rating, comment = r.Comment, userName = r.User.UserName });
            if (reviews == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(reviews);
            }

        }

        [HttpPost, Authorize]
        public IActionResult PostNewReview([FromBody] Review value)
        {
            var userId = User.FindFirstValue("id");
            value.UserId = userId;
            _context.Reviews.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);

        }

        [HttpDelete("delete/gameId_{gameId}"), Authorize]

        public IActionResult DeleteReview(int gameId)
        {
            var userId = User.FindFirstValue("id");
            var review = _context.Reviews.Where(r => (r.UserId == userId && r.GameId == gameId)).SingleOrDefault();
            if (review != null)
            {
                _context.Remove(review);
                _context.SaveChanges();
                return StatusCode(204);
            }
            else
            {
                return StatusCode(404);
            }
            
        }

        [HttpPut("edit/gameId_{gameId}"), Authorize]

        public IActionResult EditReview(int gameId, [FromBody] Review editedReview)
        {
            var userId = User.FindFirstValue("id");
            var review = _context.Reviews.Where(r => (r.UserId == userId && r.GameId == gameId)).SingleOrDefault();
            if (review != null)
            {
                _context.Remove(review);
                editedReview.UserId = userId;
                editedReview.GameId = gameId;
                _context.Add(editedReview);
                _context.SaveChanges();
                return Ok(editedReview);
            }
            else
            {
                return StatusCode(404);
            }
        }
    }
}
