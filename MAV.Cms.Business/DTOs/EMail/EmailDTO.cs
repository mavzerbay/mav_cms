using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Mail;

namespace MAV.Cms.Business.DTOs.EMail
{
    public class EmailDTO
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
        public MailPriority MailPriority { get; set; } = MailPriority.Normal;
    }
}
