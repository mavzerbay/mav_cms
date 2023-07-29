using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Business.Validators
{
    internal class CreateSlideMediaValidator : AbstractValidator<CreateSlideMediaDTO>
    {
        public CreateSlideMediaValidator()
        {
            RuleFor(x => x.BackgroundImageFile)
                .NotEmpty()
                .NotNull().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Name")));

            RuleFor(x => x.LanguageId)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Language.ControllerTitle")));
        }
    }
}