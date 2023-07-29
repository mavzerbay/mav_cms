using System;

namespace MAV.Cms.Business.DTOs
{
    public class CreatePageCommentDTO
    {
        public Guid PageId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
    }
}
