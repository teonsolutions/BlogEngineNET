using System;
using System.Threading.Tasks;
using System.Threading;
using BlogEngine.Application.Services;
using BlogEngine.Common;
using BlogEngine.Application.ViewModels;
using MediatR;
using BlogEngine.Application.Queries;

namespace BlogEngine.Application.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, GenericResult<LoginResponse>>
    {
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenSevice;
        private readonly ISecurityQueries _securityQueries;

        public LoginCommandHandler(IMediator mediator, ITokenService tokenSevice, ISecurityQueries securityQueries)
        {
            _mediator = mediator;
            _tokenSevice = tokenSevice;
            _securityQueries = securityQueries;
        }

        public async Task<GenericResult<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            GenericResult<LoginResponse> response = new GenericResult<LoginResponse>();
            string genericMessage = "User does not exist or the password is incorrect or the User does not have permissions to use this system.";

            if (string.IsNullOrEmpty(request.UserLogin))
                response.AddError("Field UserLogin is required.");

            if (string.IsNullOrEmpty(request.Password))
                response.AddError("Field Password is required.");

            if (!response.HasErrors)
            {
                var user = await _securityQueries.GetUserInfoAsync(request.UserLogin);
                Guid rolGuidDefault = Guid.Empty;
                string rolName = string.Empty;

                if (user == null)
                {
                    response.AddError(genericMessage);
                }
                else
                {
                    if (user.Password.CompareTo(request.Password) != 0)
                    {
                        response.AddError(genericMessage);
                    }
                    else
                    {
                        // get default role information                    
                        var rol = await _securityQueries.GetUserRolAsync(user.UserID);
                        if (rol != null)
                        {
                            rolGuidDefault = rol.RolGuid;
                            rolName = rol.RolName;
                        }
                        else
                        {
                            response.AddError(genericMessage);
                        }
                    }
                }


                if (!response.HasErrors)
                {
                    string token = _tokenSevice.GenerateJwtLogin(user.UserID, request.UserLogin, user.FullName, rolGuidDefault);
                    response.Data = new LoginResponse
                    {
                        Success = true,
                        Token = token,
                        RolGuid = rolGuidDefault,
                        UserLogin = user.UserLogin,
                        FullName = user.FullName,
                        RolName = rolName
                    };
                }

            }

            return response;
        }

    }
}
