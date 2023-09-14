using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Core.Entities
{
    public class Contact : BaseEntity
    {
        public string? Name { get; set; }
        public string? Message { get; set; }
        public string? Email { get; set; }
        public string? Adress { get; set; }
        public string? OpenTime { get; set; }
        public string? Phone { get; set; }
    }
}
