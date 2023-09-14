using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Core.Entities
{
    public class Blog : BaseEntity
    {
        public string? Title { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }

        public int OwnerId { get; set; }

        //Relation Property


        public Owner? Owner { get; set; }
        public BlogDetail? BlogDetails { get; set; }
    }
}
