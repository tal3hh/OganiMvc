using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Core.Entities
{
    public class Owner : BaseEntity
    {
        public string? Fullname { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
        public string? Profession { get; set; }


        //Relation Property

        public List<Blog>? Blogs { get; set; }
    }
}
