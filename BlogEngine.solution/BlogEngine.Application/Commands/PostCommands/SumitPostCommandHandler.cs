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
    public class SumitPostCommandHandler : IRequestHandler<SubmitPostCommand, GenericResult<PostGeneratedResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IPostRepository _postRepository;

        public SumitPostCommandHandler(IMediator mediator, IPostRepository postRepository)
        {
            _mediator = mediator;
            _postRepository = postRepository;
        }

        public async Task<GenericResult<PostGeneratedResponse>> Handle(SubmitPostCommand request, CancellationToken cancellationToken)
        {
            GenericResult<PostGeneratedResponse> response = new GenericResult<PostGeneratedResponse>();
            var post = _postRepository.GetById(request.PostID);

            /* business rule: Submit posts (Writer)
             *  When a Writer submits a post, the post should move to a “pending approval” 
                   status where it’s locked and cannot be updated
            */
            if (post != null)
            {
                if (post.CreationUser != request.UserName)
                    response.AddError("You can only submit your Posts. This Post was created by other user.");

                if (post.Status != PostStatusEnum.PENDING.GetHashCode())
                    response.AddError("it only can be submitted Posts with status [Pending].");
            }
            else
            {
                response.AddError("Post not found.");
            }


            if (!response.HasErrors)
            {
                post.Status = PostStatusEnum.PENDING_APROVAL.GetHashCode();
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
