using MAV.Cms.Common.BaseModels;
using System;

namespace MAV.Cms.Business.DTOs.SpecDTOs
{
    public class GetAllPageCommentSpecDTO : BaseSpecParams
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public Guid? PageId { get; set; }
        public bool? isPublished { get; set; }
        public bool? IncludePage { get; set; }
    }
}
