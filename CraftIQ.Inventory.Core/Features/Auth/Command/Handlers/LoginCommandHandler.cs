using CraftIQ.Inventory.Core.Features.Auth.Command.Models;
using CraftIQ.Inventory.Core.IAuthServices;
using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Features.Auth.Command.Handlers
{
    public class LoginCommandHandler : ResponseHandler,
        IRequestHandler<LoginCommand, Response<AuthResponse>>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Response<AuthResponse>> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var result = await _authService.Login(
                new LoginRequest(
                    request.Email,
                    request.Password));

            return Success(result);
        }
    }
}
