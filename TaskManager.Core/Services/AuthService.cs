using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManager.Application.DTOs;
using TaskManager.Application.ServicesContract;
using TaskManager.Domain.Identities;
using Microsoft.EntityFrameworkCore;
namespace TaskManager.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<RegisterDto> Register(RegisterDto registerDto)
        {
            var user = new User
            {
                Name=registerDto.UserName,
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            await _userManager.AddToRoleAsync(user, "User");
            if (!result.Succeeded)
            {
                throw new Exception("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            return registerDto;
        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                throw new Exception("Invalid username or password.");
            }
            
            var roles = await _userManager.GetRolesAsync(user);
            string JwtToken = GenerateJwtToken(user,roles);
            string refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["RefreshToken:Expiration_Minutes"])
            );

            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                UserId = user.Id.ToString(),
                Token = JwtToken,
                Expiration = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:Expiration_Minutes"])),

                RefreshToken = refreshToken,
                RefreshTokenExpiration = user.RefreshTokenExpiryTime
            };
        }

        public async Task<AuthResponseDto> RefreshToken( string refreshToken)
        {
            var user = _userManager.Users
                .FirstOrDefault(u =>u.RefreshToken == refreshToken);

            if (user == null)
                throw new Exception("No user found");

            if ( user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new Exception("Invalid refresh token");

           var newJwtToken = GenerateJwtToken(user, await _userManager.GetRolesAsync(user));
           var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["RefreshToken:Expiration_Minutes"])
            );
            await _userManager.UpdateAsync(user);
            return new AuthResponseDto
            {
                UserId = user.Id.ToString(),
                Token = newJwtToken,
                Expiration = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:Expiration_Minutes"])),

                RefreshToken = newRefreshToken,
                RefreshTokenExpiration = user.RefreshTokenExpiryTime
            };

        }

        private string GenerateJwtToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
              new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(
                roles.Select(role => new Claim(ClaimTypes.Role, role))
            );

            DateTime expiration = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:Expiration_Minutes"])
            );

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            byte[] bytes = new byte[64];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
    }
