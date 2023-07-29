using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Business.Validators
{
    public class CreateMenuValidator : AbstractValidator<CreateMenuDTO>
    {
        public CreateMenuValidator()
        {
            RuleFor(x => x.RouterLink)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Menu.RouterLink")));

            When(x => x.IsBackend != true, () =>
            {
                RuleFor(x => x.MenuPositionId)
                    .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Menu.MenuPosition")));
            });

            RuleForEach(x => x.MenuTrans).Where(x => LangManager.IsPrimaryLanguage(x.LanguageId)).SetValidator(new CreateMenuTransValidator());
        }
    }
}
