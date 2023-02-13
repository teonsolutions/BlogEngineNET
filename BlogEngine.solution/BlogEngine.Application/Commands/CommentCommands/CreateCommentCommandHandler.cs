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
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, GenericResult<CommentCreatedResponse>>
    {
        private readonly IMediator _mediator;
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        public CreateCommentCommandHandler(IMediator mediator, ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _mediator = mediator;
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        public async Task<GenericResult<CommentCreatedResponse>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            GenericResult<CommentCreatedResponse> response = new GenericResult<CommentCreatedResponse>();

            var post = _postRepository.GetById(request.PostID);
            if (post == null)
                response.AddError($"Post {request.PostID} not found to add comment.");

            if (string.IsNullOrEmpty(request.Comments))
                response.AddError("Field Comments is required.");

            if (!response.HasErrors)
            {
                Comment comment = new Comment
                {
                    PostID = request.PostID,
                    Comments = request.Comments,
                    CommentType = CommentType.GENERAL.GetHashCode(),
                    CreationUser = request.UserName,
                    CreationDate = DateTime.Now
                };
                _commentRepository.Add(comment);
                await _commentRepository.UnitOfWork.SaveChangesAsync();

                response.Data = new CommentCreatedResponse { CommentID = comment.Id };
            }
            return response;
        }
    }
}
