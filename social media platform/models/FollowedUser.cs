using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace social_media_platform.models
{
    internal class FollowedUser
    {
        public long UserId {  get; set; }
        public long FollowedUserId { get; set;}
    }
}
