using BlogEngine.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Domain.AggregatesModel
{
    public class Rol : Entity, IAggregateRoot
    {
        public string RolName { get; set; }
        public Guid Guid { get; set; }
    }
}
