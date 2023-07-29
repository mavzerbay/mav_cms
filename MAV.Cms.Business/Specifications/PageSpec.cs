using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Extensions;
using System;
using System.Linq;

namespace MAV.Cms.Business.Specifications
{
    public class PageSpec : BaseSpecification<Domain.Entities.Page>
    {
        public PageSpec(GetAllPageSpecDTO spec)
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
            AddOrderByDescending(x => x.CreatedDate);

            AddInclude(x => x.ParentPage);
            AddInclude(x => x.PageType);
            AddInclude(x => x.Category);
            AddInclude(x => x.PageTrans);

            if (spec.IncludeChild.HasValue && spec.IncludeChild.Value)
                AddInclude(x => x.ChildPageList);

            if (spec.IncludeCreatedBy.HasValue && spec.IncludeCreatedBy.Value)
                AddInclude(x => x.CreatedBy);

            if (!spec.isAll.HasValue || (spec.isAll.HasValue && !spec.isAll.Value))
                ApplyPaging(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);

            if (spec.Sort.HasValue())
            {
                switch (spec.Sort)
                {
                    case "pageTrans.nameAsc":
                        if (spec.LanguageId.HasValue)
                            AddOrderBy(x => x.PageTrans.FirstOrDefault(l => l.LanguageId == spec.LanguageId.Value).Name);
                        break;
                    case "pageTrans.nameDesc":
                        if (spec.LanguageId.HasValue)
                            AddOrderByDescending(x => x.PageTrans.FirstOrDefault(l => l.LanguageId == spec.LanguageId.Value).Name);
                        break;
                    case "parentPage.nameAsc":
                        if (spec.LanguageId.HasValue)
                            AddOrderBy(x => x.ParentPage.PageTrans.FirstOrDefault(l => l.LanguageId == spec.LanguageId.Value).Name);
                        break;
                    case "parentPage.nameDesc":
                        if (spec.LanguageId.HasValue)
                            AddOrderByDescending(x => x.ParentPage.PageTrans.FirstOrDefault(l => l.LanguageId == spec.LanguageId.Value).Name);
                        break;
                    case "category.nameAsc":
                        if (spec.LanguageId.HasValue)
                            AddOrderBy(x => x.Category.CategoryTrans.FirstOrDefault(l => l.LanguageId == spec.LanguageId.Value).Name);
                        break;
                    case "category.nameDesc":
                        if (spec.LanguageId.HasValue)
                            AddOrderByDescending(x => x.Category.CategoryTrans.FirstOrDefault(l => l.LanguageId == spec.LanguageId.Value).Name);
                        break;
                    case "activityAsc":
                        AddOrderBy(x => x.Activity);
                        break;
                    case "activityDesc":
                        AddOrderByDescending(x => x.Activity);
                        break;
                    case "createdDateAsc":
                        AddOrderBy(x => x.CreatedDate);
                        break;
                    case "createdDateDesc":
                        AddOrderByDescending(x => x.CreatedDate);
                        break;
                }
            }
        }

        public PageSpec(Guid Id) : base(x => x.Id == Id)
        {
            AddInclude(x => x.ParentPage);
            AddInclude(x => x.Category);
            AddInclude(x => x.PageTrans);
            AddInclude(x => x.PageComments);
            AddInclude(x => x.PageType);
        }
        public PageSpec(string slug) : base(x => x.PageTrans.Select(s => s.Slug).Contains(slug))
        {
            AddInclude(x => x.CreatedBy);
            AddInclude(x => x.ParentPage);
            AddInclude(x => x.Category);
            AddInclude(x => x.PageTrans);
            AddInclude(x => x.PageComments);
            AddInclude(x => x.PageType);
            AddInclude(x => x.ChildPageList);
        }
    }
}
