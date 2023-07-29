using MAV.Cms.Common.BaseModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAV.Cms.Domain.Entities
{
    public class MenuTrans : BaseEntity
    {
        public Guid MenuId { get; set; }
        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }
        public Guid LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        /// <summary>
        /// Tooltip vs için
        /// </summary>
        public string Info { get; set; }
    }
}