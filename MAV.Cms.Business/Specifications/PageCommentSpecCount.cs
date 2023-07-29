using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Extensions;

namespace MAV.Cms.Business.Specifications
{
    public class PageCommentSpecCount : BaseSpecification<Domain.Entities.PageComment>
    {
        public PageCommentSpecCount(GetAllPageCommentSpecDTO spec)
            : base(x =>
                (!spec.Search.HasValue() || x.Comment.ToLower().Contains(spec.Search)) &&
                (!spec.PageId.HasValue || (spec.PageId.HasValue && x.PageId.Equals(spec.PageId.Value))) &&
                (!spec.isPublished.HasValue || x.isPublished.Equals(spec.isPublished)) &&

                (!spec.NameSurname.HasValue() ||
                         ((!spec.MatchMode.Contains("nameSurname") || spec.MatchMode.Contains("nameSurname:contains")) && (x.Name.Contains(spec.NameSurname) || x.Surname.Contains(spec.NameSurname))) ||
                         (spec.MatchMode.Contains("nameSurname:notContaines") && (!x.Name.Contains(spec.NameSurname) || !x.Surname.Contains(spec.NameSurname))) ||
                         (spec.MatchMode.Contains("nameSurname:endsWith") && (x.Name.EndsWith(spec.NameSurname) || x.Surname.EndsWith(spec.NameSurname))) ||
                         (spec.MatchMode.Contains("nameSurname:startsWith") && (x.Name.StartsWith(spec.NameSurname) || x.Surname.StartsWith(spec.NameSurname))) ||
                         (spec.MatchMode.Contains("nameSurname:equals") && (x.Name.Equals(spec.NameSurname) || x.Surname.Equals(spec.NameSurname))) ||
                         (spec.MatchMode.Contains("nameSurname:notEquals") && (!x.Name.Equals(spec.NameSurname) || !x.Surname.Equals(spec.NameSurname)))
                    ) &&

                (!spec.Email.HasValue() ||
                         ((!spec.MatchMode.Contains("email") || spec.MatchMode.Contains("email:contains")) && x.Email.Contains(spec.Email)) ||
                         (spec.MatchMode.Contains("email:notContaines") && !x.Email.Contains(spec.Email)) ||
                         (spec.MatchMode.Contains("email:endsWith") && x.Email.EndsWith(spec.Email)) ||
                         (spec.MatchMode.Contains("email:startsWith") && x.Email.StartsWith(spec.Email)) ||
                         (spec.MatchMode.Contains("email:equals") && x.Email.Equals(spec.Email)) ||
                         (spec.MatchMode.Contains("email:notEquals") && !x.Email.Equals(spec.Email))
                    )
            )
        {
        }
    }
}
