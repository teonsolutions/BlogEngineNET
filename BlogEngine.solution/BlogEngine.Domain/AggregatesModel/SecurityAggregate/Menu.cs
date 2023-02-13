using BlogEngine.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Domain.AggregatesModel
{
    public class Menu : Entity, IAggregateRoot
    {
        public string MenuName { get; set; }
        public int? MenuBaseID { get; set; }
        public string Url { get; set; }
        public Guid Guid { get; set; }
    }
}
