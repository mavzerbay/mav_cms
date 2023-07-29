using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Responses
{
    public class UploadResponse
    {
        public UploadResponse(string url = null, string errorMessage = null, bool isSuccess = false)
        {
            Url = url;
            ErrorMessage = errorMessage;
            IsSuccess = isSuccess;
        }
        public string Url { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
