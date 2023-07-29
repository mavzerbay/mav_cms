using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Extensions;
using System;
using System.Linq;

namespace MAV.Cms.Business.Specifications
{
    public class SlideSpec : BaseSpecification<Domain.Entities.Slide>
    {
        public SlideSpec(GetAllSlideSpecDTO spec)
            : base(x =>
                (!spec.Search.HasValue() || (spec.LanguageId.HasValue && x.SlideMedias != null && x.SlideMedias.Any(a => a.Title.ToLower().Contains(spec.Search)))) &&
                (!spec.Activity.HasValue || x.Activity.Equals(spec.Activity)) &&
                (!spec.MediaActivity.HasValue || x.SlideMedias.Any(x => x.Activity.Equals(spec.MediaActivity))) &&
                (!spec.isHome.HasValue || x.isHome.Equals(spec.isHome)) &&
                (!spec.SlidePositionId.HasValue || (spec.SlidePositionId.HasValue && x.SlidePositionId.Equals(spec.SlidePositionId.Value))) &&
                (!spec.PageId.HasValue || (spec.PageId.HasValue && x.PageId.Equals(spec.PageId.Value))) &&
                (!spec.LanguageId.HasValue || (spec.LanguageId.HasValue && x.SlideMedias != null && x.SlideMedias.Any(a => a.LanguageId.Equals(spec.LanguageId.Value) | a.AffectAllLanguage == true))) &&

                (!spec.Title.HasValue() || (spec.LanguageId.HasValue && x.SlideMedias != null && x.SlideMedias.Any(a => a.LanguageId == spec.LanguageId.Value &&
                         ((!spec.MatchMode.Contains("title") || spec.MatchMode.Contains("title:contains")) && a.Title.Contains(spec.Title)) ||
                         (spec.MatchMode.Contains("title:notContaines") && !a.Title.Contains(spec.Title)) ||
                         (spec.MatchMode.Contains("title:endsWith") && a.Title.EndsWith(spec.Title)) ||
                         (spec.MatchMode.Contains("title:startsWith") && a.Title.StartsWith(spec.Title)) ||
                         (spec.MatchMode.Contains("title:equals") && a.Title.Equals(spec.Title)) ||
                         (spec.MatchMode.Contains("title:notEquals") && !a.Title.Equals(spec.Title)))
                    )) &&

                (!spec.Name.HasValue() ||
                         ((!spec.MatchMode.Contains("name") || spec.MatchMode.Contains("name:contains")) && x.Name.ToLower().Contains(spec.Name.ToLower())) ||
                         (spec.MatchMode.Contains("name:notContaines") && !x.Name.ToLower().Contains(spec.Name.ToLower())) ||
                         (spec.MatchMode.Contains("name:endsWith") && x.Name.ToLower().EndsWith(spec.Name.ToLower())) ||
                         (spec.MatchMode.Contains("name:startsWith") && x.Name.ToLower().StartsWith(spec.Name.ToLower())) ||
                         (spec.MatchMode.Contains("name:equals") && x.Name.ToLower().Equals(spec.Name.ToLower())) ||
                         (spec.MatchMode.Contains("name:notEquals") && !x.Name.ToLower().Equals(spec.Name.ToLower()))
                    )
            )
        {
            AddOrderByDescending(x => x.CreatedDate);

            AddInclude(x => x.SlideMedias);
            AddInclude(x => x.SlidePosition);

            if (!spec.isAll.HasValue || (spec.isAll.HasValue && !spec.isAll.Value))
                ApplyPaging(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);

            if (spec.Sort.HasValue())
            {
                switch (spec.Sort)
                {
                    case "titleAsc":
                        if (spec.LanguageId.HasValue)
                            AddOrderBy(x => x.SlideMedias.FirstOrDefault(l => l.LanguageId == spec.LanguageId.Value).Title);
                        break;
                    case "titleDesc":
                        if (spec.LanguageId.HasValue)
                            AddOrderByDescending(x => x.SlideMedias.FirstOrDefault(l => l.LanguageId == spec.LanguageId.Value).Title);
                        break;
                    case "activityAsc":
                        AddOrderBy(x => x.Activity);
                        break;
                    case "activityDesc":
                        AddOrderByDescending(x => x.Activity);
                        break;
                }
            }
        }

        public SlideSpec(Guid Id, Guid? languageId, bool isPageId = false) : base(x => isPageId ? x.PageId == Id : x.Id == Id && (!languageId.HasValue || x.SlideMedias.Any(a => a.LanguageId == languageId)))
        {
            AddInclude(x => x.SlideMedias);
            AddInclude(x => x.SlidePosition);
            if (!isPageId)
                AddInclude(x => x.Page);
        }
    }
}
