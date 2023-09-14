using FluentValidation;
using OganiApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.FluentValidations
{
    public class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kateqoriya'un adini yazin.").NotNull().WithMessage("Kateqoriya'un adini yazin.")
                .Length(2, 100).WithMessage("Kateqoriya adi 2-100 simvol olmalidir.");

            RuleFor(x => x.Photo).NotNull().WithMessage("Kateqoriya ucun sekil secin...").NotEmpty().WithMessage("Kateqoriya ucun sekil secin...");

           
        }
    }
}
