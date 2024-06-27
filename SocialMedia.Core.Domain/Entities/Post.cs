using SocialMedia.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Domain.Entities
{
    public class Post : AuditableBaseEntity
    {
        public string? VisualContentPath { get; set; }
        public string? Descripcion { get; set; }
        public string? VisualContentType { get; set; }
        public string UserId { get; set; }
        public virtual IEnumerable<Comment>  Comments { get; set; }
    }
}
