using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Extensions;
using System.Linq;

namespace MAV.Cms.Business.Specifications
{
    public class CustomVarSpecCount : BaseSpecification<Domain.Entities.CustomVar>
    {
        public CustomVarSpecCount(GetAllCustomVarSpecDTO spec)
            : base(x =>
                (!spec.Search.HasValue() || (spec.LanguageId.HasValue && x.CustomVarTrans != null && x.CustomVarTrans.Any(a => a.LanguageId.Equals(spec.LanguageId) && a.Name.ToLower().Contains(spec.Search)) || x.GroupName.ToLower().Contains(spec.Search) || x.KeyName.ToLower().Contains(spec.Search))) &&
                (!spec.GroupName.HasValue() || x.GroupName.Equals(spec.GroupName)) &&
                (!spec.KeyName.HasValue() || x.KeyName.Equals(spec.KeyName)) &&
                (!spec.LanguageId.HasValue || (spec.LanguageId.HasValue && x.CustomVarTrans != null && x.CustomVarTrans.Any(a => a.LanguageId.Equals(spec.LanguageId.Value)))) &&

                (!spec.Name.HasValue() || (!spec.LanguageId.HasValue && x.CustomVarTrans != null && x.CustomVarTrans.Any(a => a.LanguageId == spec.LanguageId.Value &&
                         ((!spec.MatchMode.Contains("name") || spec.MatchMode.Contains("name:contains")) && a.Name.Contains(spec.Name)) ||
                         (spec.MatchMode.Contains("name:notContaines") && !a.Name.Contains(spec.Name)) ||
                         (spec.MatchMode.Contains("name:endsWith") && a.Name.EndsWith(spec.Name)) ||
                         (spec.MatchMode.Contains("name:startsWith") && a.Name.StartsWith(spec.Name)) ||
                         (spec.MatchMode.Contains("name:equals") && a.Name.Equals(spec.Name)) ||
                         (spec.MatchMode.Contains("name:notEquals") && !a.Name.Equals(spec.Name)))
                    )) &&

                (!spec.Description.HasValue() || (!spec.LanguageId.HasValue && x.CustomVarTrans != null && x.CustomVarTrans.Any(a => a.LanguageId == spec.LanguageId.Value &&
                         ((!spec.MatchMode.Contains("description") || spec.MatchMode.Contains("description:contains")) && a.Description.Contains(spec.Description)) ||
                         (spec.MatchMode.Contains("description:notContaines") && !a.Description.Contains(spec.Description)) ||
                         (spec.MatchMode.Contains("description:endsWith") && a.Description.EndsWith(spec.Description)) ||
                         (spec.MatchMode.Contains("description:startsWith") && a.Description.StartsWith(spec.Description)) ||
                         (spec.MatchMode.Contains("description:equals") && a.Description.Equals(spec.Description)) ||
                         (spec.MatchMode.Contains("description:notEquals") && !a.Description.Equals(spec.Description)))
                    ))
            )
        {
        }
    }
}
