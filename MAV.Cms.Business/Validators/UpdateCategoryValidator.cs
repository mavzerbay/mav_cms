using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Business.Validators
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDTO>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Category.ControllerTitle")));

            RuleForEach(x => x.CategoryTrans).Where(x => LangManager.IsPrimaryLanguage(x.LanguageId)).SetValidator(new UpdateCategoryTransValidator());
        }
    }
}
