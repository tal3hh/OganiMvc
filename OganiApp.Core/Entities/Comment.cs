using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Core.Entities
{
    public class Comment : BaseEntity
    {
        public string? Message { get; set; }
        public string? ByName { get; set; }
        public int ProductId { get; set; }

        //Relation Property

        public Product? Product { get; set; }
    }
}
