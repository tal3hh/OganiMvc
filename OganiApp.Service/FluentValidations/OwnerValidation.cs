using FluentValidation;
using OganiApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.FluentValidations
{
    public class OwnerValidation : AbstractValidator<Owner>
    {
        public OwnerValidation()
        {
            RuleFor(x => x.Fullname).NotEmpty().WithMessage("Adini yazin.").NotNull().WithMessage("Adini yazin.")
                .Length(2, 100).WithMessage("2-100 simvol olmalidir.");

            RuleFor(x => x.Profession).NotEmpty().WithMessage("Pese adini yazin.").NotNull().WithMessage("Pese adini yazin.")
                .Length(2, 100).WithMessage("2-100 simvol olmalidir.");

            RuleFor(x => x.Photo).NotNull().WithMessage("Sekil secin...").NotEmpty().WithMessage("Sekil secin...");
        }
    }
}
