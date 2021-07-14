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
        
        [ForeignKey("Platform")]
        public int PlatformId { get; set; }
        public PlatformID Platform { get; set; }
    }
}
