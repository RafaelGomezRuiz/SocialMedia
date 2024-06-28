using SocialMedia.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Domain.Entities
{
    public class Friend : AuditableBaseEntity
    {
        public string UserId { get; set; }
        //public List<string> FriendsIds { get; set; }
    }
}
