using Assistt.Application.Commands;
using Assistt.Application.Services.Auth;
using Assistt.Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Application.CommandHandlers
{
    public class UserCommandHandlers : IRequestHandler<UserCommands.UserLogin, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IAuthenticationService _authenticationService;



        public UserCommandHandlers(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }

        public Task<string> Handle(UserCommands.UserLogin request, CancellationToken cancellationToken)
        {
          var userControl = _unitOfWork.Users.Any(x => x.Email == request.EMail && x.Password == request.Password);

            if (userControl)
            {
                var token = _authenticationService.GenerateToken(request.EMail);
                return Task.FromResult(token);
            }
            else
            {
                throw new Exception("Kullanıcı adı veya şifre hatalı");
            }
           
        }
    }
}
