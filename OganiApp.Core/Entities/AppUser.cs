using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Core.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public List<Favorite>? Favorites { get; set; }
        public List<Basket>? Baskets { get; set; }
    }
}
