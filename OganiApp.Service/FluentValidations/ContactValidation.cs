using FluentValidation;
using FluentValidation.Validators;
using OganiApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.FluentValidations
{
    public class ContactValidation : AbstractValidator<Contact>
    {
        public ContactValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email yazin.").NotNull().WithMessage("Email yazin.")
                .Length(2, 150).WithMessage("2-150 simvol olmalidir.");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Adini yazin.").NotNull().WithMessage("Adini yazin.")
                .Length(2, 150).WithMessage("2-150 simvol olmalidir.");

            RuleFor(x => x.Message).NotEmpty().WithMessage("Message yazin.").NotNull().WithMessage("Adini yazin.")
                .Length(2, 300).WithMessage("2-300 simvol olmalidir.");
        }
    }
}
