using CraftIQ.Inventory.Base;
using CraftIQ.Inventory.Core.Features.Auth.Command.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : AppControllerBase
    {
        [HttpPost(Routes.AuthRoutes.Login)]
        public async Task<IActionResult> Login(
            [FromBody] LoginCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPost(Routes.AuthRoutes.Register)]
        public async Task<IActionResult> Register(
            [FromBody] RegisterCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPost(Routes.AuthRoutes.RefreshToken)]
        public async Task<IActionResult> RefreshToken(
            [FromBody] RefreshTokenCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPost(Routes.AuthRoutes.ForgotPassword)]
        public async Task<IActionResult> ForgotPassword(
            [FromBody] ForgotPasswordCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPost(Routes.AuthRoutes.ResetPassword)]
        public async Task<IActionResult> ResetPassword(
            [FromBody] ResetPasswordCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
    }
}
