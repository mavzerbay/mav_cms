using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Extensions;
using System;

namespace MAV.Cms.Business.Specifications
{
    public class LanguageSpec : BaseSpecification<Domain.Entities.Language>
    {
        public LanguageSpec(GetAllLanguageSpecDTO spec)
            : base(x => 
                (!spec.Search.HasValue() || x.Name.ToLower().Contains(spec.Search)) &&
                (!spec.isRTL.HasValue || x.isRTL.Equals(spec.isRTL)) &&
                (!spec.isPrimary.HasValue || x.isPrimary.Equals(spec.isPrimary)) &&
                (!spec.Activity.HasValue || x.Activity.Equals(spec.Activity)) &&
                (!spec.DisplayOrder.HasValue || x.DisplayOrder.Equals(spec.DisplayOrder.Value)) &&

                (!spec.Name.HasValue() ||
                         ((!spec.MatchMode.Contains("name") || spec.MatchMode.Contains("name:contains")) && x.Name.Contains(spec.Name)) ||
                         (spec.MatchMode.Contains("name:notContaines") && !x.Name.Contains(spec.Name)) ||
                         (spec.MatchMode.Contains("name:endsWith") && x.Name.EndsWith(spec.Name)) ||
                         (spec.MatchMode.Contains("name:startsWith") && x.Name.StartsWith(spec.Name)) ||
                         (spec.MatchMode.Contains("name:equals") && x.Name.Equals(spec.Name)) ||
                         (spec.MatchMode.Contains("name:notEquals") && !x.Name.Equals(spec.Name))
                    ) &&

                (!spec.Culture.HasValue() ||
                         ((!spec.MatchMode.Contains("culture") || spec.MatchMode.Contains("culture:contains")) && x.Culture.Contains(spec.Culture)) ||
                         (spec.MatchMode.Contains("culture:notContaines") && !x.Culture.Contains(spec.Culture)) ||
                         (spec.MatchMode.Contains("culture:endsWith") && x.Culture.EndsWith(spec.Culture)) ||
                         (spec.MatchMode.Contains("culture:startsWith") && x.Culture.StartsWith(spec.Culture)) ||
                         (spec.MatchMode.Contains("culture:equals") && x.Culture.Equals(spec.Culture)) ||
                         (spec.MatchMode.Contains("culture:notEquals") && !x.Culture.Equals(spec.Culture))
                    ) &&

                (!spec.FlagIcon.HasValue() ||
                         ((!spec.MatchMode.Contains("flagIcon") || spec.MatchMode.Contains("flagIcon:contains")) && x.FlagIcon.Contains(spec.FlagIcon)) ||
                         (spec.MatchMode.Contains("flagIcon:notContaines") && !x.FlagIcon.Contains(spec.FlagIcon)) ||
                         (spec.MatchMode.Contains("flagIcon:endsWith") && x.FlagIcon.EndsWith(spec.FlagIcon)) ||
                         (spec.MatchMode.Contains("flagIcon:startsWith") && x.FlagIcon.StartsWith(spec.FlagIcon)) ||
                         (spec.MatchMode.Contains("flagIcon:equals") && x.FlagIcon.Equals(spec.FlagIcon)) ||
                         (spec.MatchMode.Contains("flagIcon:notEquals") && !x.FlagIcon.Equals(spec.FlagIcon))
                    )
            )
        {
            AddOrderBy(x => x.DisplayOrder);

            if (!spec.isAll.HasValue || (spec.isAll.HasValue && !spec.isAll.Value))
                ApplyPaging(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);

            if (spec.Sort.HasValue())
            {
                switch (spec.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(x => x.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(x => x.Name);
                        break;
                    case "cultureAsc":
                        AddOrderBy(x => x.Culture);
                        break;
                    case "cultureDesc":
                        AddOrderByDescending(x => x.Culture);
                        break;
                    case "flagIconAsc":
                        AddOrderBy(x => x.FlagIcon);
                        break;
                    case "flagIconDesc":
                        AddOrderByDescending(x => x.FlagIcon);
                        break;
                    case "isRTLAsc":
                        AddOrderBy(x => x.isRTL);
                        break;
                    case "isRTLDesc":
                        AddOrderByDescending(x => x.isRTL);
                        break;
                    case "activityAsc":
                        AddOrderBy(x => x.Activity);
                        break;
                    case "activityDesc":
                        AddOrderByDescending(x => x.Activity);
                        break;
                    case "displayOrderAsc":
                        AddOrderBy(x => x.DisplayOrder);
                        break;
                    case "displayOrderDesc":
                        AddOrderByDescending(x => x.DisplayOrder);
                        break;
                }
            }
        }

        public LanguageSpec(Guid Id) : base(x => x.Id == Id)
        {
        }
    }
}
