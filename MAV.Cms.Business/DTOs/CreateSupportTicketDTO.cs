using System;

namespace MAV.Cms.Business.DTOs
{
    public class CreateSupportTicketDTO
    {
        /// <summary>
        /// GroupName SupportType olanlar listelenir. (Offer,Contact,Ask etc.)
        /// </summary>
        public Guid SupportTypeId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Content { get; set; }
        public bool IsClosed { get; set; } = false;
    }
}
