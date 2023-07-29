using System.Collections.Generic;

namespace MAV.Cms.Common.BaseModels
{
    public class ApiException : ApiErrorResponse
    {
        public ApiException(int statusCode, string message = null, string details = null, Dictionary<string, string> errors = null) : base(statusCode, message)
        {
            Details = details;
            Errors = errors;
        }

        public string Details { get; set; }

        public Dictionary<string, string> Errors { get; set; }
    }
}
