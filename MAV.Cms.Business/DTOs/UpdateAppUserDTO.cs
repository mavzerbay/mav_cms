using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace MAV.Cms.Business.DTOs
{
    public class UpdateAppUserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string ProfileImagePath { get; set; }
        public IFormFile ProfileImageFile { get; set; }
        public virtual ICollection<Guid> UserRoleIdList { get; set; }
    }
}
