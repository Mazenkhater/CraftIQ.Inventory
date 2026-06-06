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
    public class ResetPasswordCommandHandler : ResponseHandler,
                                                 IRequestHandler<ResetPasswordCommand, Response<string>>
    {
        private readonly IAuthService _authService;

        public ResetPasswordCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Response<string>> Handle(
            ResetPasswordCommand request,
            CancellationToken cancellationToken)
        {
            await _authService.ResetPassword(
                new ResetPassword(      
                     request.Email,
                     request.Token,
                     request.NewPassword
                ));

            return Success("Password reset successfully");
        }
    }
}
