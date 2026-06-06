using CraftIQ.Inventory.Core.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.IJwtTokenService
{
    public interface IJwtTokenService
    {
        string GenerateToken(AppUser user);
        RefreshToken GenerateRefreshToken();
    }
}
