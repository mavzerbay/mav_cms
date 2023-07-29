using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Business.Validators
{
    public class UpdatePageValidator : AbstractValidator<UpdatePageDTO>
    {
        public UpdatePageValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Page.ControllerTitle")));

            RuleForEach(x => x.PageTrans).Where(x => LangManager.IsPrimaryLanguage(x.LanguageId)).SetValidator(new UpdatePageTransValidator());

            RuleFor(x => x.PageTypeId)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Page.PageType")));
        }
    }
}
