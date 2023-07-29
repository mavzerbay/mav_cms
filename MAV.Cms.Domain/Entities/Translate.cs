using MAV.Cms.Common.BaseModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAV.Cms.Domain.Entities
{
    public class Translate : BaseEntity
    {
        public Guid LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        public string KeyName { get; set; }
        public string Translation { get; set; }
        public string Explanation { get; set; }
    }
}