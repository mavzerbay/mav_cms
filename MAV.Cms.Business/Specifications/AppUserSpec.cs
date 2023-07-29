using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Extensions;
using System;
using System.Linq;

namespace MAV.Cms.Business.Specifications
{
    public class AppUserSpec : BaseSpecification<Domain.Entities.Identity.MavUser>
    {
        public AppUserSpec(GetAllAppUserSpecDTO spec)
            : base(x =>
                (!spec.Search.HasValue() || (x.Name.ToLower().Contains(spec.Search) || x.Surname.ToLower().Contains(spec.Search) || x.UserName.ToLower().Contains(spec.Search))) &&
                (!spec.RoleId.HasValue || x.UserRoles.Select(x => x.RoleId).Contains(spec.RoleId.Value)) &&

                (!spec.NameSurname.HasValue() ||
                         ((!spec.MatchMode.Contains("nameSurname") || spec.MatchMode.Contains("nameSurname:contains")) && (x.Name.ToLower().Contains(spec.NameSurname.ToLower()) || x.Surname.ToLower().Contains(spec.NameSurname.ToLower()))) ||
                         (spec.MatchMode.Contains("nameSurname:notContaines") && (!x.Name.ToLower().Contains(spec.NameSurname.ToLower()) || !x.Surname.ToLower().Contains(spec.NameSurname.ToLower()))) ||
                         (spec.MatchMode.Contains("nameSurname:endsWith") && (x.Name.ToLower().EndsWith(spec.NameSurname.ToLower()) || x.Surname.ToLower().EndsWith(spec.NameSurname.ToLower()))) ||
                         (spec.MatchMode.Contains("nameSurname:startsWith") && (x.Name.ToLower().StartsWith(spec.NameSurname.ToLower()) || x.Surname.ToLower().StartsWith(spec.NameSurname.ToLower()))) ||
                         (spec.MatchMode.Contains("nameSurname:equals") && (x.Name.Equals(spec.NameSurname) || x.Surname.Equals(spec.NameSurname))) ||
                         (spec.MatchMode.Contains("nameSurname:notEquals") && (!x.Name.Equals(spec.NameSurname) || !x.Surname.Equals(spec.NameSurname)))
                    ) &&

                (!spec.UserName.HasValue() ||
                         ((!spec.MatchMode.Contains("userName") || spec.MatchMode.Contains("userName:contains")) && x.UserName.Contains(spec.UserName)) ||
                         (spec.MatchMode.Contains("userName:notContaines") && !x.UserName.Contains(spec.UserName)) ||
                         (spec.MatchMode.Contains("userName:endsWith") && x.UserName.EndsWith(spec.UserName)) ||
                         (spec.MatchMode.Contains("userName:startsWith") && x.UserName.StartsWith(spec.UserName)) ||
                         (spec.MatchMode.Contains("userName:equals") && x.UserName.Equals(spec.UserName)) ||
                         (spec.MatchMode.Contains("userName:notEquals") && !x.UserName.Equals(spec.UserName))
                    ) &&

                (!spec.Email.HasValue() ||
                         ((!spec.MatchMode.Contains("email") || spec.MatchMode.Contains("email:contains")) && x.Email.Contains(spec.Email)) ||
                         (spec.MatchMode.Contains("email:notContaines") && !x.Email.Contains(spec.Email)) ||
                         (spec.MatchMode.Contains("email:endsWith") && x.Email.EndsWith(spec.Email)) ||
                         (spec.MatchMode.Contains("email:startsWith") && x.Email.StartsWith(spec.Email)) ||
                         (spec.MatchMode.Contains("email:equals") && x.Email.Equals(spec.Email)) ||
                         (spec.MatchMode.Contains("email:notEquals") && !x.Email.Equals(spec.Email))
                    ) &&

                (!spec.PhoneNumber.HasValue() ||
                         ((!spec.MatchMode.Contains("phoneNumber") || spec.MatchMode.Contains("phoneNumber:contains")) && x.PhoneNumber.Contains(spec.PhoneNumber)) ||
                         (spec.MatchMode.Contains("phoneNumber:notContaines") && !x.PhoneNumber.Contains(spec.PhoneNumber)) ||
                         (spec.MatchMode.Contains("phoneNumber:endsWith") && x.PhoneNumber.EndsWith(spec.PhoneNumber)) ||
                         (spec.MatchMode.Contains("phoneNumber:startsWith") && x.PhoneNumber.StartsWith(spec.PhoneNumber)) ||
                         (spec.MatchMode.Contains("phoneNumber:equals") && x.PhoneNumber.Equals(spec.PhoneNumber)) ||
                         (spec.MatchMode.Contains("phoneNumber:notEquals") && !x.PhoneNumber.Equals(spec.PhoneNumber))
                    )
            )
        {
            AddOrderByDescending(x => x.CreatedDate);

            if (!spec.isAll.HasValue || (spec.isAll.HasValue && !spec.isAll.Value))
                ApplyPaging(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);

            if (spec.Sort.HasValue())
            {
                switch (spec.Sort)
                {
                    case "nameSurnameAsc":
                        AddOrderBy(x => x.Name);
                        AddThenOrderBy(x => x.Surname);
                        break;
                    case "nameSurnameDesc":
                        AddOrderByDescending(x => x.Name);
                        AddThenOrderByDescending(x => x.Surname);
                        break;
                    case "userNameAsc":
                        AddOrderBy(x => x.UserName);
                        break;
                    case "userNameDesc":
                        AddOrderByDescending(x => x.UserName);
                        break;
                    case "emailAsc":
                        AddOrderBy(x => x.Email);
                        break;
                    case "emailDesc":
                        AddOrderByDescending(x => x.Email);
                        break;
                    case "phoneNumberAsc":
                        AddOrderBy(x => x.PhoneNumber);
                        break;
                    case "phoneNumberDesc":
                        AddOrderByDescending(x => x.PhoneNumber);
                        break;
                }
            }
        }

        public AppUserSpec(Guid Id) : base(x => x.Id == Id)
        {
            AddInclude(x => x.UserRoles);
        }
    }
}
