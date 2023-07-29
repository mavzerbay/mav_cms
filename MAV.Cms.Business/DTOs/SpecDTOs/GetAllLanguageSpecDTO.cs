using MAV.Cms.Common.BaseModels;

namespace MAV.Cms.Business.DTOs.SpecDTOs
{
    public class GetAllLanguageSpecDTO : BaseSpecParams
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public int? DisplayOrder { get; set; }
        public string FlagIcon { get; set; }
        public bool? Activity { get; set; }
        public bool? isRTL { get; set; }
        public bool? isPrimary { get; set; }
    }
}
