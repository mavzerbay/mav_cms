using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Business.Validators
{
    public class CreatePageCommentValidator : AbstractValidator<CreatePageCommentDTO>
    {
        public CreatePageCommentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Name")));

            RuleFor(x => x.Surname)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Surname")));

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Email")));

            RuleFor(x => x.Comment)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("PageComment.Comment")));

            RuleFor(x => x.PageId)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Page.ControllerTitle")));
        }
    }
}
