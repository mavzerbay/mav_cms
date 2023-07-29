using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Helpers;
using System.Linq;

namespace MAV.Cms.Business.Validators
{
    public class UpdateSlideValidator : AbstractValidator<UpdateSlideDTO>
    {
        public UpdateSlideValidator()
        {
            RuleFor(x => x.SlidePositionId)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Slide.SlidePosition")));

            When(x => x.SlideMedias == null || (x.SlideMedias != null && !x.SlideMedias.Any()), () =>
                  {
                      RuleFor(x => x.SlideMedias)
                          .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Slide.SlideMedias")));
                  });

            RuleForEach(x => x.SlideMedias).Where(x => LangManager.IsPrimaryLanguage(x.LanguageId)).SetValidator(new UpdateSlideMediaValidator());
        }
    }
}
