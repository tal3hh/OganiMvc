using FluentValidation;
using OganiApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.FluentValidations
{
    public class BlogDetailValidator : AbstractValidator<BlogDetail>
    {
        public BlogDetailValidator()
        {
            RuleFor(x => x.BlogId).Must(x => x != 0).WithMessage("Blog secin! Yoxdursa yeni bir blog elave edin.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Adini yazin.").NotNull().WithMessage("Adini yazin.")
                .Length(2, 300).WithMessage("2-300 simvol olmalidir.");

            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Adini yazin.").NotNull().WithMessage("Adini yazin.")
                .Length(2, 100).WithMessage("2-100 simvol olmalidir.");

            RuleFor(x => x.Tags).NotEmpty().WithMessage("Adini yazin.").NotNull().WithMessage("Adini yazin.")
                .Length(2, 100).WithMessage("2-100 simvol olmalidir.");
        }
    }
}
