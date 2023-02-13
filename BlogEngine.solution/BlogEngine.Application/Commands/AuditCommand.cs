using System;
using System.Runtime.Serialization;

namespace BlogEngine.Application.Commands
{
    [DataContract]
    public class AuditCommand
    {
        [DataMember]
        public string UserName { get; set; }
    }
}
