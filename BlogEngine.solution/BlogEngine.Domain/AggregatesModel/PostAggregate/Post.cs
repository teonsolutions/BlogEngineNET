using BlogEngine.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Domain.AggregatesModel
{
    public class Post: Entity, IAggregateRoot
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime? RejectedDate { get; set; }
    }
}
