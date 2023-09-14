using FluentValidation;
using OganiApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.FluentValidations
{
    public class CommentValidation : AbstractValidator<Comment>
    {
        public CommentValidation()
        {
            RuleFor(x => x.ByName).NotEmpty().WithMessage("ByName'un adini yazin.").NotNull().WithMessage("ByName'un adini yazin.")
                .Length(2, 100).WithMessage("ByName adi 2-100 simvol olmalidir.");

            RuleFor(x => x.Message).NotEmpty().WithMessage("Message'un adini yazin.").NotNull().WithMessage("Message'un adini yazin.")
                .Length(2, 200).WithMessage("Message adi 2-200 simvol olmalidir.");
        }
    }
}
