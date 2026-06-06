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
    public class RegisterCommandHandler : ResponseHandler,
                                           IRequestHandler<RegisterCommand, Response<string>>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Response<string>> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            await _authService.Register(
                new RegisterRequest(request.UserName,request.Email,request.Password));

            return Success("User Registered Successfully");
        }
    }
}
