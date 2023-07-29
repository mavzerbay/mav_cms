using MAV.Cms.Business.DTOs.UploadDTOs;
using MAV.Cms.Business.Interfaces;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Services
{
    public class UploadService : IUploadService
    {
        private readonly ILogger<UploadService> _logger;
        private IHttpContextAccessor _httpContextAccessor => new HttpContextAccessor();
        public UploadService(ILogger<UploadService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gönderilen yoldaki dosyayı siler
        /// </summary>
        /// <param name="path">Veritabanına kaydedilen yol /Resources... </param>
        public void Delete(string path)
        {
            try
            {
                var deletePath = Path.Combine(Directory.GetCurrentDirectory(), path);
                if (File.Exists(deletePath))
                {
                    File.Delete(deletePath);
                    _logger.LogInformation($"{deletePath} yolundaki dosya silindi");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task<UploadResponse> UploadAsync(CreateUploadDTO dto)
        {
            try
            {
                var folderName = Path.Combine("Resources", dto.ModelName, dto.DataId.ToString());
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (dto.File != null && dto.File.Length > 0)
                {
                    var extension = Path.GetExtension(dto.File.FileName);
                    var fileName = $"{Guid.NewGuid()}{extension}";
                    if (dto.OverriteName.HasValue())
                    {
                        fileName = $"{dto.OverriteName}{extension}";
                    }

                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = $"\\{Path.Combine(folderName, fileName)}";

                    if (!Directory.Exists(pathToSave))
                        Directory.CreateDirectory(pathToSave);

                    if (dto.ReplacePath.HasValue())
                    {
                        Delete(dto.ReplacePath);
                    }

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await dto.File.CopyToAsync(stream);
                        stream.Flush();
                        return new UploadResponse(dbPath.Replace("\\", "/"), null, true);
                    }
                }

                return new UploadResponse(null, "Dosya yüklemesi başarısız", false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new UploadResponse(null, ex.Message, false);
            }
        }
    }
}
