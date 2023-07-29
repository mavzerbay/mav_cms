using MAV.Cms.Business.DTOs.EMail;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Services
{
    public class MailService : IMailService
    {
        private readonly ICustomVarService _customVarService;
        private readonly ILogger<MailService> _logger;

        public MailService(ICustomVarService customVarService, ILogger<MailService> logger)
        {
            _customVarService = customVarService ?? throw new ArgumentNullException(nameof(customVarService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> SendMailAsync(EmailDTO request)
        {
            try
            {
                //Keynames = Mail,From,Password,Host,Port
                var mailSettings = await _customVarService.GetAllCustomVar("MailSettings");

                string senderMail = null;
                string from = "Klasik Merdiven";
                string senderPassword = null;
                string host = null;// smtp.gmail.com
                int? port = 587;
                bool ssl = false;
                if (mailSettings.IsSuccess && mailSettings.DataMulti != null && mailSettings.DataMulti.Any())
                {
                    if (mailSettings.DataMulti.Any(x => x.KeyName == "SenderMail"))
                        senderMail = mailSettings.DataMulti.FirstOrDefault(x => x.KeyName == "SenderMail").Value;
                    if (mailSettings.DataMulti.Any(x => x.KeyName == "SenderPassword"))
                        senderPassword = mailSettings.DataMulti.FirstOrDefault(x => x.KeyName == "SenderPassword").Value;
                    if (mailSettings.DataMulti.Any(x => x.KeyName == "Host"))
                        host = mailSettings.DataMulti.FirstOrDefault(x => x.KeyName == "Host").Value;
                    if (mailSettings.DataMulti.Any(x => x.KeyName == "From"))
                        from = mailSettings.DataMulti.FirstOrDefault(x => x.KeyName == "From").Value;
                    if (mailSettings.DataMulti.Any(x => x.KeyName == "Port"))
                        port = int.Parse(mailSettings.DataMulti.FirstOrDefault(x => x.KeyName == "Port").Value);
                    if (mailSettings.DataMulti.Any(x => x.KeyName == "Ssl"))
                        ssl = bool.Parse(mailSettings.DataMulti.FirstOrDefault(x => x.KeyName == "Ssl").Value);
                }

                if (senderMail != null && from != null && senderPassword != null && host != null && port.HasValue)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(request.ToEmail);
                    mail.From = new MailAddress(senderMail, from, System.Text.Encoding.UTF8);
                    mail.Subject = request.Subject;
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    mail.Body = request.Body;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.Priority = request.MailPriority;

                    if (request.Attachments != null && request.Attachments.Any())
                    {

                        for (int i = 0; i < request.Attachments.Count; i++)
                        {
                            if (request.Attachments[i].Length > 0)
                            {
                                using (var stream = request.Attachments[i].OpenReadStream())
                                {
                                    mail.Attachments.Add(new Attachment(stream, request.Attachments[i].FileName));
                                }
                            }
                        }
                    }

                    SmtpClient client = new SmtpClient(host,port.Value);
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(senderMail, senderPassword);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = ssl;

                    client.Send(mail);
                    return true;
                }
                else
                    throw new Exception("Mail ayarları bulunamadı");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;

            }
        }
    }
}
