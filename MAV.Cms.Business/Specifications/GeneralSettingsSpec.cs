using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Common.BaseModels;
using System.Linq;

namespace MAV.Cms.Business.Specifications
{
    public class GeneralSettingsSpec : BaseSpecification<Domain.Entities.GeneralSettings>
    {
        public GeneralSettingsSpec(GetAllGeneralSettingsSpecDTO spec)
            : base(x =>
                (!spec.LanguageId.HasValue || (spec.LanguageId.HasValue && x.GeneralSettingsTrans != null && x.GeneralSettingsTrans.Any(a => a.LanguageId.Equals(spec.LanguageId.Value))))
            )
        {
            AddOrderByDescending(x => x.CreatedDate);

            AddInclude(x => x.TestimonialSlide);
            AddInclude(x => x.LatestProjectPage);
            AddInclude(x => x.GeneralSettingsTrans);

            if (!spec.isAll.HasValue || (spec.isAll.HasValue && !spec.isAll.Value))
                ApplyPaging(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);            
        }
    }
}
