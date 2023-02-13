using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Application.ViewModels
{
    public class PostGeneratedResponse
    {
        public int PostID { get; set; }
    }

    public class PostListResponse
    {
        public int PostID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public DateTime? PublishedDate { get; set; }
    }

    public class PostListRequest
    {
        public Guid RolGuid { get; set; }
        public string UserName { get; set; }
    }

    public class PostResponse
    {
        public int PostID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Status { get; set; }
        public DateTime CreationDate { get; set; }
        public IEnumerable<CommentResponse> Comments { get; set; }
    }
}
