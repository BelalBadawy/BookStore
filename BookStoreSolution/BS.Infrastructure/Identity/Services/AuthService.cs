using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BS.Application.Interfaces;
using BS.Domain.Common;
using BS.Domain.Entities;
using BS.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BS.Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        public Guid? UserId => null;


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;


        public AuthService(
            UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null)
            {
              //  throw new Exception($"User with {loginModel.Email} not found.");
              return new Response<AuthenticationResponse>($"User with {loginModel.Email} not found.");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, loginModel.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return new Response<AuthenticationResponse>($"Credentials for '{loginModel.Email} aren't valid'.");
                //throw new Exception($"Credentials for '{loginModel.Email} aren't valid'.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            AuthenticationResponse response = new AuthenticationResponse
            {
                Id = user.Id.ToString(),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName
            };

            return new Response<AuthenticationResponse>(response);
        }

        public async Task<Response<Guid>> RegisterAsync(RegistrationModel request)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                FullName = request.FullName,
                EmailConfirmed = false,
                UserName = request.Email
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return new Response<Guid>(user.Id);
                }
                else
                {
                    return new Response<Guid>($"{string.Join("; ", result.Errors.Select(o=>o.Description).ToList())}");
                }
            }
            else
            {
                return new Response<Guid>($"Email {request.Email } already exists.");
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            userClaims.Add(new Claim(CustomClaimTypes.Permission,AppPermissions.AppClaim.List));

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                //  expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
