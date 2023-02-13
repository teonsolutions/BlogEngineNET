using BlogEngine.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Domain.AggregatesModel
{
    public class Status: Entity, IAggregateRoot
    {
        public string Description { get; set; }
    }
}
