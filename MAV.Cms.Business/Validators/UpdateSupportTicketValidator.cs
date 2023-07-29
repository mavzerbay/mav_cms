using FluentValidation;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Common.Extensions;
using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Business.Validators
{
    public class UpdateSupportTicketValidator : AbstractValidator<UpdateSupportTicketDTO>
    {
        public UpdateSupportTicketValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("SupportTicket.ControllerTitle")));

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Name")));

            RuleFor(x => x.Surname)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Surname")));

            When(x => !x.Email.HasValue(), () =>
            {
                RuleFor(x => x.PhoneNumber)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.PhoneNumber")));

            });

            When(x => !x.PhoneNumber.HasValue(), () =>
            {
                RuleFor(x => x.Email)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Common.Email")));

            });
            RuleFor(x => x.Content)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("SupportTicket.Comment")));

            RuleFor(x => x.SupportTypeId)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(LangManager.Translate("Common.Required"), LangManager.Translate("Page.ControllerTitle")));
        }
    }
}
