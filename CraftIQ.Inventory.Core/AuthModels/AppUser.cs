using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.AuthModels
{
    public class AppUser:IdentityUser<Guid>
    {
        public List<RefreshToken> RefreshTokens { get; set; } = new();
    }
}
