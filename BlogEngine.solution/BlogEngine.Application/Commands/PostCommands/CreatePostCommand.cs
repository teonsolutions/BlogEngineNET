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
    public class CreatePostCommand : AuditCommand, IRequest<GenericResult<PostGeneratedResponse>>
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
