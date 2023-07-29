using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Business.Validators
{
    public class CreatePageValidator : AbstractValidator<CreatePageDTO>
    {
        public CreatePageValidator()
        {
            RuleForEach(x => x.PageTrans).Where(x => LangManager.IsPrimaryLanguage(x.LanguageId)).SetValidator(new CreatePageTransValidator());

            RuleFor(x => x.PageTypeId)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Page.PageType")));
        }
    }
}
