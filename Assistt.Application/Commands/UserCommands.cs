using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Application.Commands
{
    public class UserCommands
    {
        public class UserLogin : IRequest<bool>
        {
            public string EMail { get; set; }
            public string Password { get; set; }

        }
    }
}
