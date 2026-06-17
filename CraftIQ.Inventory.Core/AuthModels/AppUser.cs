using Microsoft.AspNetCore.Identity;

namespace CraftIQ.Inventory.Core.AuthModels
{
    public class AppUser:IdentityUser<Guid>
    {
        public List<RefreshToken> RefreshTokens { get; set; } = new();
    }
}
