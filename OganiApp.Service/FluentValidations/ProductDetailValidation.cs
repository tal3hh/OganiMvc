using FluentValidation;
using OganiApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.FluentValidations
{
    public class ProductDetailValidation : AbstractValidator<ProductDetail>
    {
        public ProductDetailValidation()
        {
            RuleFor(x => x.ProductId).Must(x => x != 0).WithMessage("Mehsul secin! Yoxdursa yeni bir mehsul elave edin.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Mehsul'un adini yazin.").NotNull().WithMessage("Mehsul'un adini yazin.")
                .Length(2, 400).WithMessage("Mehsul adi 2-400 simvol olmalidir.");

            RuleFor(x => x.Weight).NotNull().WithMessage("Ceki daxil edin.");

            RuleFor(x => x.StarCount).NotNull().WithMessage("Mehsul sayini daxil edin.");

            RuleFor(x => x.StarCount).Must(x => x != 0).WithMessage("Say daxil edin.");
        }
    }
}
