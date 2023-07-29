using System;

namespace MAV.Cms.Business.DTOs
{
    public class UpdatePageCommentDTO
    {
        public Guid Id { get; set; }
        public Guid PageId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public bool isPublished { get; set; } = false;
    }
}
