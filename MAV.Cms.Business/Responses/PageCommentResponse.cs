using MAV.Cms.Common.BaseModels;
using System;

namespace MAV.Cms.Business.Responses
{
    public class PageCommentResponse : BaseResponse
    {
        public Guid PageId { get; set; }
        public virtual PageResponse Page { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public bool isPublished { get; set; }
    }
}
