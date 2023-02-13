using BlogEngine.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Domain.AggregatesModel
{
    public class User: Entity, IAggregateRoot
    {
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}
