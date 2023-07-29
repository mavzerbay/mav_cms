using MAV.Cms.Common.BaseModels;

namespace MAV.Cms.Domain.Entities
{
    public class Language : BaseEntity
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public string FlagIcon { get; set; }
        public bool isRTL { get; set; }
        public bool Activity { get; set; }
        public bool isPrimary { get; set; }
        public int DisplayOrder { get; set; }
    }
}