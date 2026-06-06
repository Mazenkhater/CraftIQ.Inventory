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
    public class ForgotPasswordCommandHandler : ResponseHandler,
                                                    IRequestHandler<ForgotPasswordCommand, Response<string>>
    {
        private readonly IAuthService _authService;

        public ForgotPasswordCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Response<string>> Handle(
            ForgotPasswordCommand request,
            CancellationToken cancellationToken)
        {
            await _authService.ForgotPassword(new ForgotPassword(request.Email));
                

            return Success("Reset password email sent");
        }
    }
}
