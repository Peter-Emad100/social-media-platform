using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace social_media_platform.models
{
    internal class User
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }

        // Users this user is following
        [InverseProperty(nameof(FollowedUser.Follower))]
        public ICollection<FollowedUser> FollowedUsers { get; set; } = new List<FollowedUser>();

        // Users following this user
        [InverseProperty(nameof(FollowedUser.Followed))]
        public ICollection<FollowedUser> Followers { get; set; } = new List<FollowedUser>();
    }

}
