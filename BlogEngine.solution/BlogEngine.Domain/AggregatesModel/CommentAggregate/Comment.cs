using BlogEngine.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Domain.AggregatesModel
{
    public class Comment : Entity, IAggregateRoot
    {
        public int PostID { get; set; }
        public string Comments { get; set; }
        public int CommentType { get; set; }
    }
}
