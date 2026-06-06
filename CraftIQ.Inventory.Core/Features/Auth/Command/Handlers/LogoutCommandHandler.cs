using CraftIQ.Inventory.Core.Features.Auth.Command.Models;
using CraftIQ.Inventory.Core.IAuthServices;
using CraftIQ.Inventory.Core.ResponseBases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Features.Auth.Command.Handlers
{
    public class LogoutCommandHandler
     : ResponseHandler,
       IRequestHandler<LogoutCommand, Response<string>>
    {
        private readonly IAuthService _authService;

        public LogoutCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Response<string>> Handle(
            LogoutCommand request,
            CancellationToken cancellationToken)
        {
            await _authService.Logout(request.refreshToken);

            return Success("Logged out successfully");
        }
    }
}
