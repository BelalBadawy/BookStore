using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BS.API.Infrastructure;
using BS.Application.Dtos;
using BS.Application.Interfaces;
using BS.Application.Services.Interfaces;
using BS.Domain.Common;
using BS.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BS.API.Controllers
{
    [ApiVersion("1.0")]
    public class AccountController : BaseApiController
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthService authService, ILogger<AccountController> logger)
        {
            _authService = authService;
            _logger = logger;
            _logger.LogInformation($"Enter the {nameof(AccountController)} controller");
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAccount(RegistrationModel registrationModel)
        {

            if (registrationModel == null)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.RegisterAsync(registrationModel);

            if (response != null)
            {
                if (response.Succeeded)
                {
                    return Ok(response.Data);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            else
            {
                response = new Response<Guid>(SD.ErrorOccurred);
                return BadRequest(response);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {

            if (loginModel == null)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.AuthenticateAsync(loginModel);

            if (response != null)
            {
                if (response.Succeeded)
                {
                    return Ok(response.Data);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            else
            {
              
                return BadRequest(response);
            }
        }
    }
}
