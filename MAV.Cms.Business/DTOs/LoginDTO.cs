namespace MAV.Cms.Business.DTOs
{
    public class LoginDTO
    {
        public string EmailOrUserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
