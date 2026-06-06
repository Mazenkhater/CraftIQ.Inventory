using CraftIQ.Inventory.Core.ResponseBases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Features.Auth.Command.Models
{
    public class ForgotPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public ForgotPasswordCommand(string email)
        {
            Email = email;
        }
    }
}
