using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Business.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryValidator()
        {
            RuleForEach(x => x.CategoryTrans).Where(x => LangManager.IsPrimaryLanguage(x.LanguageId)).SetValidator(new CreateCategoryTransValidator());
        }
    }
}
