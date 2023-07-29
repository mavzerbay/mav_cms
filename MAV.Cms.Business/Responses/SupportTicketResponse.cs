using MAV.Cms.Common.BaseModels;
using System;

namespace MAV.Cms.Business.Responses
{
    public class SupportTicketResponse : BaseResponse
    {
        /// <summary>
        /// GroupName SupportType olanlar listelenir. (Offer,Contact,Ask etc.)
        /// </summary>
        public Guid SupportTypeId { get; set; }
        public virtual CustomVarResponse SupportType { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NameSurname
        {
            get
            {
                return Name + " " + Surname;
            }
        }
        public string Content { get; set; }
        public bool IsClosed { get; set; } = false;
        /// <summary>
        /// Mail Gönderildi mi bilgisi
        /// </summary>
        public bool MailSended { get; set; } = false;
    }
}
