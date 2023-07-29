using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Extensions;
using System;
using System.Linq;

namespace MAV.Cms.Business.Specifications
{
    public class TranslateSpec : BaseSpecification<Domain.Entities.Translate>
    {
        public TranslateSpec(GetAllTranslateSpecDTO spec)
            : base(x =>
                (!spec.Search.HasValue() || x.KeyName.ToLower().Contains(spec.Search) || x.Translation.ToLower().Contains(spec.Search)) &&
                (!spec.LanguageId.HasValue || x.LanguageId.Equals(spec.LanguageId)) &&

                (!spec.KeyName.HasValue() || (spec.LanguageId.HasValue && x.LanguageId.Equals(spec.LanguageId) &&
                         ((!spec.MatchMode.Contains("keyName") || spec.MatchMode.Contains("keyName:contains")) && x.KeyName.Contains(spec.KeyName)) ||
                         (spec.MatchMode.Contains("keyName:notContaines") && !x.KeyName.Contains(spec.KeyName)) ||
                         (spec.MatchMode.Contains("keyName:endsWith") && x.KeyName.EndsWith(spec.KeyName)) ||
                         (spec.MatchMode.Contains("keyName:startsWith") && x.KeyName.StartsWith(spec.KeyName)) ||
                         (spec.MatchMode.Contains("keyName:equals") && x.KeyName.Equals(spec.KeyName)) ||
                         (spec.MatchMode.Contains("keyName:notEquals") && !x.KeyName.Equals(spec.KeyName))
                    )) &&

                (!spec.Translation.HasValue() || (spec.LanguageId.HasValue && x.LanguageId.Equals(spec.LanguageId) &&
                         ((!spec.MatchMode.Contains("translation") || spec.MatchMode.Contains("translation:contains")) && x.Translation.Contains(spec.Translation)) ||
                         (spec.MatchMode.Contains("translation:notContaines") && !x.Translation.Contains(spec.Translation)) ||
                         (spec.MatchMode.Contains("translation:endsWith") && x.Translation.EndsWith(spec.Translation)) ||
                         (spec.MatchMode.Contains("translation:startsWith") && x.Translation.StartsWith(spec.Translation)) ||
                         (spec.MatchMode.Contains("translation:equals") && x.Translation.Equals(spec.Translation)) ||
                         (spec.MatchMode.Contains("translation:notEquals") && !x.Translation.Equals(spec.Translation))
                    )) &&

                (!spec.Explanation.HasValue() || (spec.LanguageId.HasValue && x.LanguageId.Equals(spec.LanguageId) &&
                         ((!spec.MatchMode.Contains("explanation") || spec.MatchMode.Contains("explanation:contains")) && x.Explanation.Contains(spec.Explanation)) ||
                         (spec.MatchMode.Contains("explanation:notContaines") && !x.Explanation.Contains(spec.Explanation)) ||
                         (spec.MatchMode.Contains("explanation:endsWith") && x.Explanation.EndsWith(spec.Explanation)) ||
                         (spec.MatchMode.Contains("explanation:startsWith") && x.Explanation.StartsWith(spec.Explanation)) ||
                         (spec.MatchMode.Contains("explanation:equals") && x.Explanation.Equals(spec.Explanation)) ||
                         (spec.MatchMode.Contains("explanation:notEquals") && !x.Explanation.Equals(spec.Explanation))
                    ))
            )
        {
            AddOrderByDescending(x => x.CreatedDate);

            AddInclude(x => x.Language);

            if (!spec.isAll.HasValue || (spec.isAll.HasValue && !spec.isAll.Value))
                ApplyPaging(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);

            if (spec.Sort.HasValue())
            {
                switch (spec.Sort)
                {
                    case "language.nameAsc":
                            AddOrderBy(x => x.Language.Name);
                        break;
                    case "language.nameDesc":
                            AddOrderByDescending(x => x.Language.Name);
                        break;
                    case "keyNameAsc":
                        AddOrderBy(x => x.KeyName);
                        break;
                    case "keyNameDesc":
                        AddOrderByDescending(x => x.KeyName);
                        break;
                    case "translationAsc":
                        AddOrderBy(x => x.Translation);
                        break;
                    case "translationDesc":
                        AddOrderByDescending(x => x.Translation);
                        break;
                    case "explanationAsc":
                        AddOrderBy(x => x.Explanation);
                        break;
                    case "explanationDesc":
                        AddOrderByDescending(x => x.Explanation);
                        break;
                }
            }
        }

        public TranslateSpec(Guid Id) : base(x => x.Id == Id)
        {
            AddInclude(x => x.Language);
        }
    }
}
