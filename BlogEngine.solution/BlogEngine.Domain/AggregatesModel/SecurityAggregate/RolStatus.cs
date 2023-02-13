using BlogEngine.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Domain.AggregatesModel
{
    public class RolStatus : Entity, IAggregateRoot
    {
        public int RolID { get; set; }
        public int StatusID { get; set; }
        public bool IsActive { get; set; }
    }
}
