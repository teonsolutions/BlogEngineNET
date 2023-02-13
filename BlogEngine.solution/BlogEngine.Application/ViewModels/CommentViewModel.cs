using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Application.ViewModels
{
    public class CommentCreatedResponse
    {
        public int CommentID { get; set; }
    }

    public class CommentResponse
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public string Comments { get; set; }
        public string CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public int CommentType { get; set; }
    }
}
