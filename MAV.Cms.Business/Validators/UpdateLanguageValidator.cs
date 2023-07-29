using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Business.Validators
{
    public class UpdateLanguageValidator : AbstractValidator<UpdateLanguageDTO>
    {
        public UpdateLanguageValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Language.ControllerTitle")));

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Name")));

            RuleFor(x => x.Culture)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Language.Culture")));

            RuleFor(x => x.FlagIcon)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Language.FlagIcon")));
        }
    }
}
