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
    public class RejectPostCommandHandler : IRequestHandler<RejectPostCommand, GenericResult<PostGeneratedResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public RejectPostCommandHandler(IMediator mediator, IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _mediator = mediator;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        public async Task<GenericResult<PostGeneratedResponse>> Handle(RejectPostCommand request, CancellationToken cancellationToken)
        {
            GenericResult<PostGeneratedResponse> response = new GenericResult<PostGeneratedResponse>();
            var post = _postRepository.GetById(request.PostID);

            /* business rule: Get, Approve or Reject pending posts (Editor)
             *  Editor should be able to include a comment when rejecting a post, and this 
                comment should be visible to the Writer only.
            */

            if (post != null)
            {
                if (post.Status != PostStatusEnum.PENDING_APROVAL.GetHashCode())
                    response.AddError("Only posts with status [Pending Approval] can be rejected.");
            }
            else
            {
                response.AddError("Post not found.");
            }


            if (!response.HasErrors)
            {
                post.Status = PostStatusEnum.REJECTED.GetHashCode();
                post.RejectedDate = DateTime.Now;
                post.UpdateUser = request.UserName;
                post.UpdateDate = DateTime.Now;

                _postRepository.Update(post);

                // add: comments
                Comment comment = new Comment
                {
                    PostID = request.PostID,
                    Comments = request.Comment,
                    CommentType = CommentType.REJECTED.GetHashCode(),
                    CreationUser = request.UserName,
                    CreationDate = DateTime.Now
                };
                _commentRepository.Add(comment);

                await _postRepository.UnitOfWork.SaveChangesAsync();

                response.Data = new PostGeneratedResponse { PostID = post.Id };
            }
            return response;
        }
    }
}
