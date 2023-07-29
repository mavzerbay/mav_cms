using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Business.Validators
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            When(x => x.EmailOrUserName.Contains("@"), () =>
            {
                RuleFor(x => x.EmailOrUserName)
                    .EmailAddress()
                    .NotNull().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Email")));
            });
            When(x => !x.EmailOrUserName.Contains("@"), () =>
            {
                RuleFor(x => x.EmailOrUserName)
                    .NotNull().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Email")));
            });

            RuleFor(x => x.Password)
                .NotNull().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Password")));
        }
    }
}
