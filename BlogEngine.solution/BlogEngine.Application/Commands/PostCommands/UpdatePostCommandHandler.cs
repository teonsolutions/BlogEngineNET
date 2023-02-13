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
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, GenericResult<PostGeneratedResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IPostRepository _postRepository;

        public UpdatePostCommandHandler(IMediator mediator, IPostRepository postRepository)
        {
            _mediator = mediator;
            _postRepository = postRepository;
        }

        public async Task<GenericResult<PostGeneratedResponse>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            GenericResult<PostGeneratedResponse> response = new GenericResult<PostGeneratedResponse>();
            var post = _postRepository.GetById(request.PostID);

            /* business rule: Get own posts, create and edit posts (Writer
             * Writers should be able to edit existing posts that haven't been published or submitted
            */
            if (post != null)
            {
                if (post.CreationUser != request.UserName)
                    response.AddError("You can only modify your Posts. This Post was created by other user.");

                if (post.Status != PostStatusEnum.PENDING.GetHashCode())
                    response.AddError("it only can be modified Posts with status [Pending].");
            }
            else
            {
                response.AddError("Post not found.");
            }


            if (!response.HasErrors)
            {
                post.Title = request.Title;
                post.Description = request.Description;
                post.UpdateUser = request.UserName;
                post.UpdateDate = DateTime.Now;

                _postRepository.Update(post);
                await _postRepository.UnitOfWork.SaveChangesAsync();

                response.Data = new PostGeneratedResponse { PostID = post.Id };
            }
            return response;
        }
    }
}
