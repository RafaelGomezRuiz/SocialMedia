using SocialMedia.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Domain.Entities
{
    public class Comment : AuditableBaseEntity
    { 
        public string? Message { get; set; }
        public string CommentType { get; set; }
        public string UserId { get; set; }
        public virtual int PostId { get; set; }
        public virtual Post Post { get; set; }

    }
}
