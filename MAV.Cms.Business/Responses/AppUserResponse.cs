using MAV.Cms.Common.BaseModels;
using System;
using System.Collections.Generic;

namespace MAV.Cms.Business.Responses
{
    public class AppUserResponse : BaseResponse
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NameSurname
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }
        public string PhoneNumber { get; set; }
        public string ProfileImagePath { get; set; }
        public virtual ICollection<BaseDropdownResponse> UserRoles { get; set; }
    }
}
