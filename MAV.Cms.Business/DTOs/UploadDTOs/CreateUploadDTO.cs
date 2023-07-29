using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAV.Cms.Business.DTOs.UploadDTOs
{
    public class CreateUploadDTO
    {
        public string ModelName { get; set; }
        public Guid DataId { get; set; }
        /// <summary>
        /// Extension olmadan isim yazılır.
        /// </summary>
        public string OverriteName { get; set; }
        public IFormFile File { get; set; }
        public string ReplacePath { get; set; }
    }
}
