using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var reviews = _context.Reviews.Where(r => r.GameId == gameId);
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
            _context.Reviews.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);

        }

    }
}
