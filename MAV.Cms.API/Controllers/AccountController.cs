
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Interfaces;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MAV.Lister.API.Controller
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        [HttpPost("Register")]
        public async Task<ApiResponse<UserResponse>> Register([FromBody] CreateAccountDTO request) => await _accountService.Create(request);

        [HttpPost("Login")]
        public async Task<ApiResponse<UserResponse>> Login([FromBody] LoginDTO request) => await _accountService.Login(request);
        [HttpGet("Logout")]
        public async Task<ApiResponse<bool>> Logout([FromQuery] LoginDTO request) => await _accountService.Logout();
    }
}
