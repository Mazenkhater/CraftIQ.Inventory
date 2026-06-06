using CraftIQ.Inventory.Core.AuthModels;
using CraftIQ.Inventory.Core.IAuthServices;
using CraftIQ.Inventory.Core.IJwtTokenService;
using CraftIQ.Inventory.Infrastructure.Data;
using CraftIQ.Inventory.Shared.Contracts.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CraftIQ.Inventory.Services.JwtService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtTokenService _jwt;
        private readonly AppDBContext _context;

        public AuthService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IJwtTokenService jwt,
            AppDBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwt = jwt;
            _context = context;
        }
        public async Task Register(RegisterRequest request)
        {
            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(",", result.Errors.Select(x => x.Description)));
        }
        public async Task<AuthResponse> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                throw new Exception("Invalid credentials");

            var check = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!check.Succeeded)
                throw new Exception("Invalid credentials");

            var accessToken = _jwt.GenerateToken(user);
            var refreshToken = _jwt.GenerateRefreshToken();

            refreshToken.UserId = user.Id;

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthResponse(accessToken,refreshToken.Token);
        }
        public async Task<AuthResponse> RefreshToken(string token)
        {
            var refresh = await _context.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == token);

            if (refresh == null ||
                refresh.IsRevoked ||
                refresh.ExpiresOn < DateTime.UtcNow)
                throw new Exception("Invalid refresh token");

            refresh.IsRevoked = true;

            var newAccess = _jwt.GenerateToken(refresh.User);
            var newRefresh = _jwt.GenerateRefreshToken();

            newRefresh.UserId = refresh.UserId;

            _context.RefreshTokens.Add(newRefresh);
            await _context.SaveChangesAsync();

            return new AuthResponse(newAccess, newRefresh.Token);
        }
        public async Task Logout(string token)
        {
            var refresh = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == token);

            if (refresh != null)
            {
                refresh.IsRevoked = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task ForgotPassword(ForgotPassword request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // هنا تبعته Email
            Console.WriteLine(token);
        }
        public async Task ResetPassword(ResetPassword request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                throw new Exception("Invalid user");

            var result = await _userManager.ResetPasswordAsync(
                user,
                request.Token,
                request.NewPassword);

            if (!result.Succeeded)
                throw new Exception("Reset failed");
        }
    }
}
