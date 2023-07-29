using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Business.Validators
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountDTO>
    {
        public CreateAccountValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotNull().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Email")));

            RuleFor(x => x.Name)
                .NotNull().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Name")));

            RuleFor(x => x.Surname)
                .NotNull().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Surname")));

            RuleFor(x => x.Password)
                .NotNull().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Password")));

            RuleFor(x => x.PasswordConfirm)
                .Equal(x => x.Password).WithMessage(LangManager.Translate("Account.PasswordNotMatch"))
                .NotNull().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.PasswordConfirm")));

            RuleFor(x => x.PhoneNumber)
                .NotNull().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.PhoneNumber")));
        }
    }
}
