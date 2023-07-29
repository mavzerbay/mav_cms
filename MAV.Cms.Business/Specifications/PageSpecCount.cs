using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Extensions;
using System;
using System.Linq;

namespace MAV.Cms.Business.Specifications
{
    public class PageSpecCount : BaseSpecification<Domain.Entities.Page>
    {
        public PageSpecCount(GetAllPageSpecDTO spec)
            : base(x =>
                (!spec.Search.HasValue() || (spec.LanguageId.HasValue && x.PageTrans != null && x.PageTrans.Any(a => a.Name.ToLower().Contains(spec.Search)))) &&
                (!spec.Activity.HasValue || x.Activity.Equals(spec.Activity.Value)) &&
                (!spec.CategoryId.HasValue || x.CategoryId.Equals(spec.CategoryId.Value)) &&
                (!spec.ParentPageId.HasValue || x.ParentPageId.Equals(spec.ParentPageId.Value)) &&
                (!spec.PageTypeId.HasValue || x.PageTypeId.Equals(spec.PageTypeId.Value)) &&
                (!spec.PageId.HasValue || !x.PageTypeId.Equals(spec.PageId.Value)) &&
                (!spec.OnlyParent.HasValue || x.ParentPageId == null) &&
                (!spec.PageTypeKeyName.HasValue() || x.PageType.KeyName.Equals(spec.PageTypeKeyName)) &&
                (!spec.LanguageId.HasValue || (spec.LanguageId.HasValue && x.PageTrans != null && x.PageTrans.Any(a => a.LanguageId.Equals(spec.LanguageId.Value)))) &&

                (spec.Slug == null || (spec.LanguageId.HasValue && x.PageTrans != null && x.PageTrans.Any(a => a.LanguageId == spec.LanguageId.Value &&
                         ((!spec.MatchMode.Contains("slug") || spec.MatchMode.Contains("slug:contains")) && a.Slug.Contains(spec.Slug)) ||
                         (spec.MatchMode.Contains("slug:notContaines") && !a.Slug.Contains(spec.Slug)) ||
                         (spec.MatchMode.Contains("slug:endsWith") && a.Slug.EndsWith(spec.Slug)) ||
                         (spec.MatchMode.Contains("slug:startsWith") && a.Slug.StartsWith(spec.Slug)) ||
                         (spec.MatchMode.Contains("slug:equals") && a.Slug.Equals(spec.Slug)) ||
                         (spec.MatchMode.Contains("slug:notEquals") && !a.Slug.Equals(spec.Slug)))
                    )) &&

                (!spec.Name.HasValue() || (spec.LanguageId.HasValue && x.PageTrans != null && x.PageTrans.Any(a => a.LanguageId == spec.LanguageId.Value &&
                         ((!spec.MatchMode.Contains("name") || spec.MatchMode.Contains("name:contains")) && a.Name.ToLower().Contains(spec.Name.ToLower())) ||
                         (spec.MatchMode.Contains("name:notContaines") && !a.Name.ToLower().Contains(spec.Name.ToLower())) ||
                         (spec.MatchMode.Contains("name:endsWith") && a.Name.ToLower().EndsWith(spec.Name.ToLower())) ||
                         (spec.MatchMode.Contains("name:startsWith") && a.Name.ToLower().StartsWith(spec.Name.ToLower())) ||
                         (spec.MatchMode.Contains("name:equals") && a.Name.ToLower().Equals(spec.Name.ToLower())) ||
                         (spec.MatchMode.Contains("name:notEquals") && !a.Name.ToLower().Equals(spec.Name.ToLower())))
                    ))
            )
        {
        }
    }
}
