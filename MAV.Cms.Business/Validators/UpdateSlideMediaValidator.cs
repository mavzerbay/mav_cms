using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Extensions;
using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Business.Validators
{
    internal class UpdateSlideMediaValidator : AbstractValidator<UpdateSlideMediaDTO>
    {
        public UpdateSlideMediaValidator()
        {
            When(x => (x.BackgroundImageFile == null || (x.BackgroundImageFile != null && x.BackgroundImageFile.Length <= 0)) && !x.BackgroundImagePath.HasValue(), () =>
            {
                RuleFor(x => x.BackgroundImagePath)
                    .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Slide.BackgroundImage")));
            });

            RuleFor(x => x.SlideId)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Slide.ControllerTitle")));

            RuleFor(x => x.LanguageId)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Language.ControllerTitle")));
        }
    }
}