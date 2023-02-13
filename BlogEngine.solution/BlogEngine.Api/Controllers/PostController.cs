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
using BlogEngine.Api.Infrastructure.Filters;
using BlogEngine.Application.Services;

namespace BlogEngine.Api.Controllers
{
    [ApiController]
    [Route("api/v1/blogengine/posts")]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly IMediator _mediator;
        private readonly IPostQueries _postQueries;
        private readonly ICommentQueries _commentQueries;
        private readonly ISessionService _sessionService;
        public PostController(ILogger<PostController> logger,
                IMediator mediator,
                IPostQueries postQueries,
                ICommentQueries commentQueries,
                ISessionService sessionService)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _postQueries = postQueries ?? throw new ArgumentNullException(nameof(postQueries));
            _commentQueries = commentQueries ?? throw new ArgumentNullException(nameof(commentQueries));
            _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<PostListResponse>), (int)HttpStatusCode.OK)]
        [AuthorizeCheckActionFilter()]
        public async Task<IActionResult> GetAllPosts([FromQuery] PaginatedRequest<PostListRequest> request)
        {
            if (request.Filter == null) request.Filter = new PostListRequest();
            var session = _sessionService.GetSession();
            if (!string.IsNullOrEmpty(session.RolGuid))
                request.Filter.RolGuid = Guid.Parse(session.RolGuid);
            if (!string.IsNullOrEmpty(session.UserLogin))
                request.Filter.UserName = session.UserLogin;
            var posts = await _postQueries.GetAllPostsAsync(request);
            return Ok(posts);
        }

        [HttpGet("{postID}")]
        [ProducesResponseType(typeof(PostResponse), (int)HttpStatusCode.OK)]
        [AuthorizeCheckActionFilter()]
        public async Task<IActionResult> GetByID(int postID)
        {
            var post = await _postQueries.GetByIDAsync(postID);
            if (post != null)
            {
                var comments = await _commentQueries.GetAllCommentsByPostIDAsync(postID);
                post.Comments = comments;
            }
            return Ok(post);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(GenericResult<PostGeneratedResponse>), (int)HttpStatusCode.OK)]
        [AuthorizeCheckActionFilter()]
        public async Task<IActionResult> Create([FromBody] CreatePostCommand command)
        {
            var cltToken = new System.Threading.CancellationToken();
            var session = _sessionService.GetSession();
            command.UserName = session.UserLogin ?? command.UserName;
            var commandResult = await _mediator.Send(command, cltToken);
            return commandResult.HasErrors ? (IActionResult)BadRequest(commandResult) : (IActionResult)Ok(commandResult); ;
        }

        [HttpPut()]
        [ProducesResponseType(typeof(GenericResult<PostGeneratedResponse>), (int)HttpStatusCode.OK)]
        [AuthorizeCheckActionFilter()]
        public async Task<IActionResult> Update([FromBody] UpdatePostCommand command)
        {
            var cltToken = new System.Threading.CancellationToken();
            var session = _sessionService.GetSession();
            command.UserName = session.UserLogin ?? command.UserName;
            var commandResult = await _mediator.Send(command, cltToken);
            return commandResult.HasErrors ? (IActionResult)BadRequest(commandResult) : (IActionResult)Ok(commandResult); ;
        }

        [HttpPost("submit")]
        [ProducesResponseType(typeof(GenericResult<PostGeneratedResponse>), (int)HttpStatusCode.OK)]
        [AuthorizeCheckActionFilter()]
        public async Task<IActionResult> SubmitPost([FromBody] SubmitPostCommand command)
        {
            var cltToken = new System.Threading.CancellationToken();
            var session = _sessionService.GetSession();
            command.UserName = session.UserLogin ?? command.UserName;
            var commandResult = await _mediator.Send(command, cltToken);
            return commandResult.HasErrors ? (IActionResult)BadRequest(commandResult) : (IActionResult)Ok(commandResult); ;
        }

        [HttpPost("aprove")]
        [ProducesResponseType(typeof(GenericResult<PostGeneratedResponse>), (int)HttpStatusCode.OK)]
        [AuthorizeCheckActionFilter()]
        public async Task<IActionResult> AprovePost([FromBody] AprovePostCommand command)
        {
            var cltToken = new System.Threading.CancellationToken();
            var session = _sessionService.GetSession();
            command.UserName = session.UserLogin ?? command.UserName;
            var commandResult = await _mediator.Send(command, cltToken);
            return commandResult.HasErrors ? (IActionResult)BadRequest(commandResult) : (IActionResult)Ok(commandResult); ;
        }

        [HttpPost("reject")]
        [ProducesResponseType(typeof(GenericResult<PostGeneratedResponse>), (int)HttpStatusCode.OK)]
        [AuthorizeCheckActionFilter()]
        public async Task<IActionResult> RejectPost([FromBody] RejectPostCommand command)
        {
            var cltToken = new System.Threading.CancellationToken();
            var session = _sessionService.GetSession();
            command.UserName = session.UserLogin ?? command.UserName;
            var commandResult = await _mediator.Send(command, cltToken);
            return commandResult.HasErrors ? (IActionResult)BadRequest(commandResult) : (IActionResult)Ok(commandResult); ;
        }
    }
}
