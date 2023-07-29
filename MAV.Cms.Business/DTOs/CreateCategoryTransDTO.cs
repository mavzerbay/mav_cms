using System;

namespace MAV.Cms.Business.DTOs
{
    public class CreateCategoryTransDTO
    {
        public Guid? CategoryId { get; set; }
        public Guid LanguageId { get; set; }
        public string Name { get; set; }
        public string ToolTip { get; set; }
    }
}