using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Business.Validators
{
    internal class CreatePageTransValidator : AbstractValidator<CreatePageTransDTO>
    {
        public CreatePageTransValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Name")))
                .NotNull()
                .MaximumLength(100).WithMessage(string.Format(LangManager.Translate("Common.MaximumLength"), LangManager.Translate("Common.Name"), 100));

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Slug")));

            RuleFor(x => x.LanguageId)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Language.ControllerTitle")));
        }
    }
}