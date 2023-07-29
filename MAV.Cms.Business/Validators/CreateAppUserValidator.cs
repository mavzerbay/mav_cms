using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Business.Validators
{
    public class CreateAppUserValidator : AbstractValidator<CreateAppUserDTO>
    {
        public CreateAppUserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Name")));

            RuleFor(x => x.Surname)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Surname")));

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Email")));

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.PhoneNumber")));

            RuleFor(x => x.Password)
                .NotNull().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Password")));

            RuleFor(x => x.PasswordConfirm)
                .Equal(x => x.Password).WithMessage(LangManager.Translate("Account.PasswordNotMatch"))
                .NotNull().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.PasswordConfirm")));
        }
    }
}
