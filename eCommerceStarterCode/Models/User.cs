using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace eCommerceStarterCode.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ShoppingCartEntry> ShoppingCartEntries { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
