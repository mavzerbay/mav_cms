using System.Collections.Generic;

namespace MAV.Cms.Common.Helpers
{
    public class ApiResponse<T>
    {
        public ApiResponse(int pageIndex, int pageSize, int count, IReadOnlyList<T> dataMulti, int? statusCode = null, bool isSuccess = false, string message = null)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            DataMulti = dataMulti;
            Message = message;
            StatusCode = statusCode;
            IsSuccess = isSuccess;
        }
        public ApiResponse(T dataSingle, bool isSuccess = false, int? statusCode = null, string message = null)
        {
            DataSingle = dataSingle;
            Message = message;
            StatusCode = statusCode;
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; set; } = false;
        public int? StatusCode { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public string Message { get; set; }
        public T DataSingle { get; set; }
        public IReadOnlyList<T> DataMulti { get; set; }
    }
}
