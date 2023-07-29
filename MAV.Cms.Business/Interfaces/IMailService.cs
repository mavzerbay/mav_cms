using MAV.Cms.Business.DTOs.EMail;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Interfaces
{
    public interface IMailService
    {
        Task<bool> SendMailAsync(EmailDTO request);
    }
}
