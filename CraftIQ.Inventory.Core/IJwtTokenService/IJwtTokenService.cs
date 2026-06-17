using CraftIQ.Inventory.Core.AuthModels;

namespace CraftIQ.Inventory.Core.IJwtTokenService
{
    public interface IJwtTokenService
    {
        string GenerateToken(AppUser user);
        RefreshToken GenerateRefreshToken();
    }
}
