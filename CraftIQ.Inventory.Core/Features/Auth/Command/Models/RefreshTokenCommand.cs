using CraftIQ.Inventory.Core.AuthModels;
using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Features.Auth.Command.Models
{
    public class RefreshTokenCommand : IRequest<Response<AuthResponse>>
    {
        public string RefreshToken { get; set; }
        public RefreshTokenCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
