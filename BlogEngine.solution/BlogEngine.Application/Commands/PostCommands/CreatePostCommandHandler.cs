using BlogEngine.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using BlogEngine.Application.ViewModels;
using System.Threading.Tasks;
using System.Threading;
using BlogEngine.Domain.SeedWork;
using BlogEngine.Domain.AggregatesModel;

namespace BlogEngine.Application.Commands
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, GenericResult<PostGeneratedResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IPostRepository _postRepository;

        public CreatePostCommandHandler(IMediator mediator, IPostRepository postRepository)
        {
            _mediator = mediator;
            _postRepository = postRepository;
        }

        public async Task<GenericResult<PostGeneratedResponse>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            GenericResult<PostGeneratedResponse> response = new GenericResult<PostGeneratedResponse>();

            if (string.IsNullOrEmpty(request.Title))
                response.AddError("Field Title is required.");

            if (string.IsNullOrEmpty(request.Title))
                response.AddError("Field  Description is required.");

            if (string.IsNullOrEmpty(request.Title))
                response.AddError("Field UserName is required.");

            if (!response.HasErrors)
            {
                Post post = new Post
                {
                    Title = request.Title,
                    Description = request.Description,
                    Status = PostStatusEnum.PENDING.GetHashCode(),
                    CreationUser = request.UserName,
                    CreationDate = DateTime.Now
                };
                _postRepository.Add(post);
                await _postRepository.UnitOfWork.SaveChangesAsync();

                response.Data = new PostGeneratedResponse { PostID = post.Id };
            }
            return response;
        }
    }
}
