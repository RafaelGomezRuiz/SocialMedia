using SocialMedia.Core.Application.ViewModels.Comment;
using SocialMedia.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.ViewModels.Posts
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string VisualContentPath { get; set; }
        public string Descripcion { get; set; }
        public string VisualContentType { get; set; }
        public string UserId { get; set; }
        public DateTime Created { get; set; }
        public virtual IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
