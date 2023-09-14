using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Core.Entities
{
    public class Advert: BaseEntity
    {
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
