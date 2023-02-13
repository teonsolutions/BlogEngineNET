using MediatR;
using System.Runtime.Serialization;
using System;
using BlogEngine.Application.Commands;
using BlogEngine.Application.ViewModels;
using BlogEngine.Common;

namespace BlogEngine.Application.Commands
{
    [DataContract]
    public class LoginCommand : IRequest<GenericResult<LoginResponse>>
    {
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public string UserLogin { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
