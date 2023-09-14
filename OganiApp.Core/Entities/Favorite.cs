using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Core.Entities
{
    public class Favorite : BaseEntity
    {
        public string? ProductName { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }

        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
