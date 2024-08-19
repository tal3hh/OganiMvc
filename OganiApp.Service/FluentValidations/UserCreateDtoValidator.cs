using FluentValidation;
using OganiApp.Service.Models.Account;

namespace OganiApp.Service.FluentValidations
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotNull().WithMessage("İstifadəçi adı mütləqdir.")
                .MinimumLength(3).WithMessage("İstifadəçi adı ən azı 3 simvoldan ibarət olmalıdır.");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("E-poçt mütləqdir.")
                .EmailAddress().WithMessage("Keçərli e-poçt adresi tələb olunur.");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Şifrə mütləqdir.")
                .MinimumLength(5).WithMessage("Şifrə ən azı 5 simvoldan ibarət olmalıdır.");

            RuleFor(x => x.ConfrimPassword)
                .Equal(x => x.Password).WithMessage("Şifrələr eyni olmalıdır.");
        }
    }
}
