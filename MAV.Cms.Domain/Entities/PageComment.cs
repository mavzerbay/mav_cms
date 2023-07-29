using MAV.Cms.Common.BaseModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAV.Cms.Domain.Entities
{
    public class PageComment : BaseEntity
    {
        public Guid PageId { get; set; }
        [ForeignKey("PageId")]
        public Page Page { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public bool isPublished { get; set; } = false;
    }
}