using BlogEngine.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Domain.AggregatesModel
{
    public class Permission : Entity, IAggregateRoot
    {
        public int MenuID { get; set; }
        public int RolID { get; set; }
        public string ActionCode { get; set; }
        public bool IsActive { get; set; }
    }
}
