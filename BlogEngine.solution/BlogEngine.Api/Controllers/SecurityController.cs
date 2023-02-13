using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogEngine.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngine.Application.Commands;
using MediatR;
using BlogEngine.Common;
using System.Net;
using BlogEngine.Application.ViewModels;
using BlogEngine.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using BlogEngine.Application.Services;

namespace BlogEngine.Api.Controllers
{
    [ApiController]
    [Route("api/v1/blogengine/securities")]
    [Authorize]
    public class SecurityController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly IMediator _mediator;
        private readonly ISecurityQueries _securityQueries;
        private readonly ISessionService _sessionService;
        public SecurityController(ILogger<PostController> logger, 
                IMediator mediator,
                ISecurityQueries securityQueries,
                ISessionService sessionService)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _securityQueries = securityQueries ?? throw new ArgumentNullException(nameof(securityQueries));
            _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(LoginCommand command)
        {
            var cltToken = new System.Threading.CancellationToken();
            var commandResult = await this._mediator.Send(command, cltToken);
            return commandResult.HasErrors ? (IActionResult)BadRequest(commandResult) : (IActionResult)Ok(commandResult);
        }

        [HttpGet("authorizationInfo")]
        public async Task<IActionResult> GetAuthorizationInfoIn()
        {
            var session = _sessionService.GetSession();
            var authorization = await _securityQueries.GetAuthorizationInfoAsync(session.UserID, Guid.Parse(session.RolGuid));
            return Ok(authorization);
        }
    }
}
