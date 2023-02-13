using BlogEngine.Application.ViewModels;
using BlogEngine.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BlogEngine.Application.Commands
{
    [DataContract]
    public class CreateCommentCommand : AuditCommand, IRequest<GenericResult<CommentCreatedResponse>>
    {
        [DataMember]
        public int PostID { get; set; }
        [DataMember]
        public string Comments { get; set; }
    }
}
