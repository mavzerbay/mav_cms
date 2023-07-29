using MAV.Cms.Common.BaseModels;
using System;

namespace MAV.Cms.Business.DTOs.SpecDTOs
{
    public class GetAllAppUserSpecDTO : BaseSpecParams
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string NameSurname { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? RoleId { get; set; }
    }
}
