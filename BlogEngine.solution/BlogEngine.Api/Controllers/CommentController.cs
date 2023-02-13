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
using BlogEngine.Application.Services;
using BlogEngine.Api.Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;

namespace BlogEngine.Api.Controllers
{
    [ApiController]
    [Route("api/v1/blogengine/comments")]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly IMediator _mediator;
        private readonly ISessionService _sessionService;
        public CommentController(ILogger<PostController> logger, IMediator mediator, ISessionService sessionService)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
        }

        [HttpPost()]
        [ProducesResponseType(typeof(GenericResult<CommentCreatedResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateCommentCommand command)
        {
            var cltToken = new System.Threading.CancellationToken();
            var session = _sessionService.GetSession();
            command.UserName = session.UserLogin ?? command.UserName;
            var commandResult = await _mediator.Send(command, cltToken);
            return commandResult.HasErrors ? (IActionResult)BadRequest(commandResult) : (IActionResult)Ok(commandResult); ;
        }
    }
}
