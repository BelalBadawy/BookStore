using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BS.Domain.Common;
using BS.Domain.Models;


namespace BS.Application.Interfaces
{
    public interface IAuthService
    {
        Guid? UserId { get; }

        Task<Response<AuthenticationResponse>> AuthenticateAsync(LoginModel loginModel);
        Task<Response<Guid>> RegisterAsync(RegistrationModel registrationModel);
    }
}
