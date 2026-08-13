using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PosSystemApi.Data;
using PosSystemApi.DTOs;
using PosSystemApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PosSystemApi.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(
            AppDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User> RegisterAsync(RegisterRequest request)
        {
            bool exists = await _context.Users
                .AnyAsync(u => u.Email == request.Email);

            if (exists)
            {
                throw new ArgumentException(
                    "A user with this email already exists.");
            }

            User user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Role = request.Role,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(
                    request.Password)
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<string?> LoginAsync(LoginRequest request)
        {
            User? user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return null;
            }

            bool passwordValid =
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    user.PasswordHash);

            if (!passwordValid)
            {
                return null;
            }

            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.UserId.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.Name),

                new Claim(
                    ClaimTypes.Email,
                    user.Email),

                new Claim(
                    ClaimTypes.Role,
                    user.Role.ToString())
            };

            string key =
                _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException(
                    "JWT key is missing.");

            SymmetricSecurityKey securityKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key));

            SigningCredentials credentials =
                new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token =
                new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(2),
                    signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}