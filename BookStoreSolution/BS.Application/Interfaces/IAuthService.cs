using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BS.Domain.Models;


namespace BS.Application.Interfaces
{
    public interface IAuthService
    {
        Guid? UserId { get; }

        Task<AuthenticationResponse> AuthenticateAsync(LoginRequest request);
        //Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
