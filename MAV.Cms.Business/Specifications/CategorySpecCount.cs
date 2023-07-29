using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Extensions;
using System;
using System.Linq;

namespace MAV.Cms.Business.Specifications
{
    public class CategorySpecCount : BaseSpecification<Domain.Entities.Category>
    {
        public CategorySpecCount(GetAllCategorySpecDTO spec)
            : base(x =>
                (!spec.Search.HasValue() || (spec.LanguageId.HasValue && x.CategoryTrans != null && x.CategoryTrans.Any(a => a.Name.ToLower().Contains(spec.Search) || a.ToolTip.ToLower().Contains(spec.Search)))) &&
                (!spec.Activity.HasValue || x.Activity.Equals(spec.Activity.Value)) &&
                (!spec.LanguageId.HasValue || (spec.LanguageId.HasValue && x.CategoryTrans != null && x.CategoryTrans.Any(a => a.LanguageId.Equals(spec.LanguageId.Value)))) &&

                (!spec.Name.HasValue() || (spec.LanguageId.HasValue && x.CategoryTrans != null && x.CategoryTrans.Any(a => a.LanguageId == spec.LanguageId.Value &&
                         ((!spec.MatchMode.Contains("name") || spec.MatchMode.Contains("name:contains")) && a.Name.Contains(spec.Name)) ||
                         (spec.MatchMode.Contains("name:notContaines") && !a.Name.Contains(spec.Name)) ||
                         (spec.MatchMode.Contains("name:endsWith") && a.Name.EndsWith(spec.Name)) ||
                         (spec.MatchMode.Contains("name:startsWith") && a.Name.StartsWith(spec.Name)) ||
                         (spec.MatchMode.Contains("name:equals") && a.Name.Equals(spec.Name)) ||
                         (spec.MatchMode.Contains("name:notEquals") && !a.Name.Equals(spec.Name)))
                    )) &&

                (!spec.ToolTip.HasValue() || (spec.LanguageId.HasValue && x.CategoryTrans != null && x.CategoryTrans.Any(a => a.LanguageId == spec.LanguageId.Value &&
                         ((!spec.MatchMode.Contains("toolTip") || spec.MatchMode.Contains("toolTip:contains")) && a.ToolTip.Contains(spec.ToolTip)) ||
                         (spec.MatchMode.Contains("toolTip:notContaines") && !a.ToolTip.Contains(spec.ToolTip)) ||
                         (spec.MatchMode.Contains("toolTip:endsWith") && a.ToolTip.EndsWith(spec.ToolTip)) ||
                         (spec.MatchMode.Contains("toolTip:startsWith") && a.ToolTip.StartsWith(spec.ToolTip)) ||
                         (spec.MatchMode.Contains("toolTip:equals") && a.ToolTip.Equals(spec.ToolTip)) ||
                         (spec.MatchMode.Contains("toolTip:notEquals") && !a.ToolTip.Equals(spec.ToolTip)))
                    ))
            )
        {
        }
    }
}
