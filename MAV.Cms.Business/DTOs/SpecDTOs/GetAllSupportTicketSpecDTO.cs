using MAV.Cms.Common.BaseModels;
using System;

namespace MAV.Cms.Business.DTOs.SpecDTOs
{
    public class GetAllSupportTicketSpecDTO : BaseSpecParams
    {
        public Guid? SupportTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string NameSurname { get; set; }
        public bool? IsClosed { get; set; }
    }
}
