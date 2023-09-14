using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Core.Entities
{
    public class ProductDetail : BaseEntity
    {
        public string? Description { get; set; }
        public decimal Weight { get; set; }
        public int StarCount { get; set; }
        public bool Availability { get; set; }

        public int ProductId { get; set; }

        //Relation Property

        public Product? Product { get; set; }
    }
}
