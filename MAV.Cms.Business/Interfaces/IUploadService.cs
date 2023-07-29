using MAV.Cms.Business.DTOs.UploadDTOs;
using MAV.Cms.Business.Responses;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Interfaces
{
    public interface IUploadService
    {
        Task<UploadResponse> UploadAsync(CreateUploadDTO dto);
        /// <summary>
        /// Gönderilen yoldaki dosyayı siler
        /// </summary>
        /// <param name="path">Veritabanına kaydedilen yol /Resources... </param>
        void Delete(string path);
    }
}
