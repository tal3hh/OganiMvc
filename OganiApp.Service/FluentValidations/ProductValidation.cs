using FluentValidation;
using OganiApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.FluentValidations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Mehsul'un adini yazin.").NotNull().WithMessage("Mehsul'un adini yazin.")
                .Length(2, 100).WithMessage("Mehsul adi 2-100 simvol olmalidir.");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Qiymet daxil edin.");

            RuleFor(x => x.Photo).NotNull().WithMessage("Mehsul ucun sekil secin...").NotEmpty().WithMessage("Mehsul ucun sekil secin...");

            RuleFor(x => x.Count).NotNull().WithMessage("Mehsul sayini daxil edin.");

            RuleFor(x => x.Count).Must(x => x != 0).WithMessage("Say daxil edin.");
        }
    }
}
