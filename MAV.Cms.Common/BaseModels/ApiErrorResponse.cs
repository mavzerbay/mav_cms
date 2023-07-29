using MAV.Cms.Common.Helpers;

namespace MAV.Cms.Common.BaseModels
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => LangManager.Translate("Error.400"),//"Hata ,işlem tamamlanamadı."
                401 => LangManager.Translate("Error.401"),//"Yetkili değilsiniz."
                404 => LangManager.Translate("Error.404"),//"Aradığınız kaynak bulunamadı."
                422 => LangManager.Translate("Error.ValidationError"),//Doğrulama hatası
                500 => LangManager.Translate("Error.500"),//"Sunucu hatası, bir şeyler yanlış gitti hemen ilgileniyoruz."
                _ => null
            };
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}