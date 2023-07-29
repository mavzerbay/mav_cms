using MAV.Cms.Common.BaseModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAV.Cms.Domain.Entities
{
    public class CustomVarTrans : BaseEntity
    {
        public Guid CustomVarId { get; set; }
        [ForeignKey("CustomVarId")]
        public virtual CustomVar CustomVar { get; set; }
        public Guid LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}