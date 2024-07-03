using Assistt.Application.Commands;
using Assistt.Application.Services.Auth;
using Assistt.Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assistt.Application.Commands.UserCommands;

namespace Assistt.Application.CommandHandlers
{
    public class UserCommandHandlers : IRequestHandler<UserCommands.UserLogin, UserLoginResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IAuthenticationService _authenticationService;



        public UserCommandHandlers(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }

        public Task<UserLoginResponse> Handle(UserCommands.UserLogin request, CancellationToken cancellationToken)
        {
          var userControl = _unitOfWork.Users.Any(x => x.Email == request.EMail && x.Password == request.Password);

            if (userControl)
            {
                var id = _unitOfWork.Users.FirstOrDefault(x => x.Email == request.EMail).Id;
                var token = _authenticationService.GenerateAccessToken(request.EMail);
                var result = new UserLoginResponse
                {
                    Token = token,
                    UserId = id
                };

                return Task.FromResult(result);
            }
            else
            {
                throw new Exception("Kullanıcı adı veya şifre hatalı");
            }
           
        }
    }
}
