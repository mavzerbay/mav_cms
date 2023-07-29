using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.Helpers;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Interfaces
{
    public interface IAccountService
    {
        Task<ApiResponse<UserResponse>> Create(CreateAccountDTO request);
        Task<ApiResponse<UserResponse>> Login(LoginDTO request);
        Task<ApiResponse<bool>> Logout();
    }
}
