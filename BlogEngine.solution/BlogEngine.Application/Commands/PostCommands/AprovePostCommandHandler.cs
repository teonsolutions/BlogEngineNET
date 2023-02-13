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
    public class AprovePostCommandHandler : IRequestHandler<AprovePostCommand, GenericResult<PostGeneratedResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IPostRepository _postRepository;

        public AprovePostCommandHandler(IMediator mediator, IPostRepository postRepository)
        {
            _mediator = mediator;
            _postRepository = postRepository;
        }

        public async Task<GenericResult<PostGeneratedResponse>> Handle(AprovePostCommand request, CancellationToken cancellationToken)
        {
            GenericResult<PostGeneratedResponse> response = new GenericResult<PostGeneratedResponse>();
            var post = _postRepository.GetById(request.PostID);

            /* business rule: Get, Approve or Reject pending posts (Editor)
             *  Editors should be able to query for “pending” posts and approve or reject their 
                publishing. Once an Editor approves a post, it will be published and visible to all 
                roles. If the post is rejected, it will be unlocked and editable again by Writers.
            */

            if (post != null)
            {
                if (post.Status != PostStatusEnum.PENDING_APROVAL.GetHashCode())
                    response.AddError("Only posts with status [Pending Approval] can be approved.");
            } else
            {
                response.AddError("Post not found.");
            }


            if (!response.HasErrors)
            {
                post.Status = PostStatusEnum.PUBLISHED.GetHashCode();
                post.PublishedDate = DateTime.Now;
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
