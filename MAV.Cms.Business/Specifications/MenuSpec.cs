using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Extensions;
using System;
using System.Linq;

namespace MAV.Cms.Business.Specifications
{
    public class MenuSpec : BaseSpecification<Domain.Entities.Menu>
    {
        public MenuSpec(GetAllMenuSpecDTO spec)
            : base(x =>
                (spec.Search == null || (!spec.LanguageId.HasValue && x.MenuTrans != null && x.MenuTrans.Any(a => a.Name.ToLower().Contains(spec.Search) || a.Info.ToLower().Contains(spec.Search)))) &&
                (!spec.Activity.HasValue || x.Activity.Equals(spec.Activity.Value)) &&
                (!spec.IsBackend.HasValue || x.IsBackend.Equals(spec.IsBackend.Value)) &&
                (!spec.MenuPositionId.HasValue || x.MenuPositionId.Equals(spec.MenuPositionId.Value)) &&
                (!spec.ParentMenuId.HasValue || x.ParentMenuId.Equals(spec.ParentMenuId.Value)) &&
                (!spec.PageId.HasValue || x.PageId.Equals(spec.PageId.Value)) &&

                (spec.Name == null || (spec.LanguageId.HasValue && x.MenuTrans != null && x.MenuTrans.Any(a => a.LanguageId == spec.LanguageId.Value &&
                         ((!spec.MatchMode.Contains("name") || spec.MatchMode.Contains("name:contains")) && a.Name.Contains(spec.Name)) ||
                         (spec.MatchMode.Contains("name:notContaines") && !a.Name.Contains(spec.Name)) ||
                         (spec.MatchMode.Contains("name:endsWith") && a.Name.EndsWith(spec.Name)) ||
                         (spec.MatchMode.Contains("name:startsWith") && a.Name.StartsWith(spec.Name)) ||
                         (spec.MatchMode.Contains("name:equals") && a.Name.Equals(spec.Name)) ||
                         (spec.MatchMode.Contains("name:notEquals") && !a.Name.Equals(spec.Name)))
                    )) &&

                (spec.Slug == null || (spec.LanguageId.HasValue && x.MenuTrans != null && x.MenuTrans.Any(a => a.LanguageId == spec.LanguageId.Value &&
                         ((!spec.MatchMode.Contains("slug") || spec.MatchMode.Contains("slug:contains")) && a.Slug.Contains(spec.Slug)) ||
                         (spec.MatchMode.Contains("slug:notContaines") && !a.Slug.Contains(spec.Slug)) ||
                         (spec.MatchMode.Contains("slug:endsWith") && a.Slug.EndsWith(spec.Slug)) ||
                         (spec.MatchMode.Contains("slug:startsWith") && a.Slug.StartsWith(spec.Slug)) ||
                         (spec.MatchMode.Contains("slug:equals") && a.Slug.Equals(spec.Slug)) ||
                         (spec.MatchMode.Contains("slug:notEquals") && !a.Slug.Equals(spec.Slug)))
                    )) &&

                (spec.Info == null || (spec.LanguageId.HasValue && x.MenuTrans != null && x.MenuTrans.Any(a => a.LanguageId == spec.LanguageId.Value &&
                         ((!spec.MatchMode.Contains("info") || spec.MatchMode.Contains("info:contains")) && a.Info.Contains(spec.Info)) ||
                         (spec.MatchMode.Contains("info:notContaines") && !a.Info.Contains(spec.Info)) ||
                         (spec.MatchMode.Contains("info:endsWith") && a.Info.EndsWith(spec.Info)) ||
                         (spec.MatchMode.Contains("info:startsWith") && a.Info.StartsWith(spec.Info)) ||
                         (spec.MatchMode.Contains("info:equals") && a.Info.Equals(spec.Info)) ||
                         (spec.MatchMode.Contains("info:notEquals") && !a.Info.Equals(spec.Info)))
                    ))
                )
        {
            AddOrderByDescending(x => x.CreatedDate);

            AddInclude(x => x.ParentMenu);
            AddInclude(x => x.MenuPosition);
            AddInclude(x => x.MenuTrans);

            if(spec.IncludePage.HasValue && spec.IncludePage.Value)
                AddInclude(x => x.Page);

            if (!spec.isAll.HasValue || (spec.isAll.HasValue && !spec.isAll.Value))
                ApplyPaging(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);

            if (spec.Sort.HasValue())
            {
                switch (spec.Sort)
                {
                    case "nameAsc":
                        if (spec.LanguageId.HasValue)
                            AddOrderBy(x => x.MenuTrans.FirstOrDefault(l => l.LanguageId == spec.LanguageId.Value).Name);
                        break;
                    case "nameDesc":
                        if (spec.LanguageId.HasValue)
                            AddOrderByDescending(x => x.MenuTrans.FirstOrDefault(l => l.LanguageId == spec.LanguageId.Value).Name);
                        break;
                    case "infoAsc":
                        if (spec.LanguageId.HasValue)
                            AddOrderBy(x => x.MenuTrans.FirstOrDefault(l => l.LanguageId == spec.LanguageId.Value).Info);
                        break;
                    case "infoDesc":
                        if (spec.LanguageId.HasValue)
                            AddOrderByDescending(x => x.MenuTrans.FirstOrDefault(l => l.LanguageId == spec.LanguageId.Value).Info);
                        break;
                    case "activityAsc":
                        AddOrderBy(x => x.Activity);
                        break;
                    case "activityDesc":
                        AddOrderByDescending(x => x.Activity);
                        break;
                    case "isBackendAsc":
                        AddOrderBy(x => x.IsBackend);
                        break;
                    case "isBackendDesc":
                        AddOrderByDescending(x => x.IsBackend);
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

        public MenuSpec(Guid Id) : base(x => x.Id == Id)
        {
            AddInclude(x => x.MenuPosition);
            AddInclude(x => x.MenuTrans);
            AddInclude(x => x.ParentMenu);
            AddInclude(x => x.Page);
        }
    }
}
