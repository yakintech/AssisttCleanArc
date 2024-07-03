using Assistt.Application.Commands;
using Assistt.Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Application.CommandHandlers
{
    public class UserCommandHandlers : IRequestHandler<UserCommands.UserLogin, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserCommandHandlers(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UserCommands.UserLogin request, CancellationToken cancellationToken)
        {
          var userControl = _unitOfWork.Users.Any(x => x.Email == request.EMail && x.Password == request.Password);

            return Task.FromResult(userControl);
           
        }
    }
}
