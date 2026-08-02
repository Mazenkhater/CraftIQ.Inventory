using CraftIQ.Inventory.Shared.Contracts.Auth;

namespace CraftIQ.Inventory.Core.IAuthServices
{
    public interface IAuthService
    {
        Task Register(RegisterRequest request);

        Task<AuthResponse> Login(LoginRequest request);

        Task<AuthResponse> RefreshToken(string refreshToken);

        Task Logout(string refreshToken);

        Task ForgotPassword(ForgotPassword request);

        Task ResetPassword(ResetPassword request);
    }
}
