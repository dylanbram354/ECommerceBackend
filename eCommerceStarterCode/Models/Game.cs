using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceStarterCode.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public ICollection<ShoppingCartEntry> ShoppingCartEntries { get; set; }
        public ICollection<Review> Reviews { get; set; }

        [ForeignKey("Platform")]
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
