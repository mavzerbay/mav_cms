using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Business.Validators
{
    public class UpdateCustomVarValidator : AbstractValidator<UpdateCustomVarDTO>
    {
        public UpdateCustomVarValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("CustomVar.ControllerTitle")));

            RuleFor(x => x.GroupName)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("CustomVar.GroupName")));

            RuleFor(x => x.KeyName)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("CustomVar.KeyName")));

            RuleForEach(x => x.CustomVarTrans).Where(x => LangManager.IsPrimaryLanguage(x.LanguageId)).SetValidator(new UpdateCustomVarTransValidator());
        }
    }
}
