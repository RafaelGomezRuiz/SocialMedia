using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.ViewModels.Friends
{
    public class SaveFriendViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }
    }
}
