using PosSystemApi.DTOs;
using PosSystemApi.Models;

namespace PosSystemApi.Authentication
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterRequest request);

        Task<string?> LoginAsync(LoginRequest request);
    }
}