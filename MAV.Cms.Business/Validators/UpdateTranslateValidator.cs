using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Business.Validators
{
    internal class UpdateTranslateValidator : AbstractValidator<UpdateTranslateDTO>
    {
        public UpdateTranslateValidator()
        {
            RuleFor(x => x.KeyName)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Translate.KeyName")));

            RuleFor(x => x.Translation)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Translation.KeyName")));

            RuleFor(x => x.LanguageId)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Language.ControllerTitle")));
        }
    }
}