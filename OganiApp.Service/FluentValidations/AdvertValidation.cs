using FluentValidation;
using OganiApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.FluentValidations
{
    public class AdvertValidation : AbstractValidator<Advert>
    {
        public AdvertValidation()
        {
            RuleFor(x => x.Photo).NotNull().WithMessage("Sekil secin...").NotEmpty().WithMessage("Sekil secin...");
        }
    }
}
