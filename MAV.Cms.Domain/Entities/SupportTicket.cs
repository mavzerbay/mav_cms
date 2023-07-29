using MAV.Cms.Common.BaseModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAV.Cms.Domain.Entities
{
    public class SupportTicket : BaseEntity
    {
        /// <summary>
        /// GroupName SupportType olanlar listelenir. (Offer,Contact,Ask etc.)
        /// </summary>
        public Guid SupportTypeId { get; set; }
        [ForeignKey("SupportTypeId")]
        public CustomVar SupportType { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Content { get; set; }
        public bool IsClosed { get; set; } = false;
    }
}