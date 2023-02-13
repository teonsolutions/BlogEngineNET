using BlogEngine.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Domain.AggregatesModel
{
    public class RolUser: Entity, IAggregateRoot
    {
        public int RolID { get; set; }
        public int UserID { get; set; }
    }
}
