using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Extensions;
using System;

namespace MAV.Cms.Business.Specifications
{
    public class SupportTicketSpec : BaseSpecification<Domain.Entities.SupportTicket>
    {
        public SupportTicketSpec(GetAllSupportTicketSpecDTO spec)
            : base(x =>
                (!spec.Search.HasValue() || (x.Name.ToLower().Contains(spec.Search) || x.Surname.ToLower().Contains(spec.Search))) &&
                (!spec.SupportTypeId.HasValue || x.SupportTypeId.Equals(spec.SupportTypeId.Value)) &&
                (!spec.IsClosed.HasValue || x.IsClosed.Equals(spec.IsClosed.Value)) &&

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

            AddInclude(x => x.SupportType);

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
                    case "nameSurnameAsc":
                        AddOrderBy(x => x.Name);
                        AddThenOrderBy(x => x.Surname);
                        break;
                    case "nameSurnameDesc":
                        AddOrderByDescending(x => x.Name);
                        AddThenOrderByDescending(x => x.Surname);
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

        public SupportTicketSpec(Guid Id) : base(x => x.Id == Id)
        {
            AddInclude(x => x.SupportType);
        }
    }
}
