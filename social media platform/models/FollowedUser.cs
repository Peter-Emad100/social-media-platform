using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace social_media_platform.models
{
    internal class FollowedUser
    {
        // Foreign Key to the follower (user who follows someone)

        [ForeignKey(nameof(Follower))]
        public long FollowerId { get; set; }
        public User Follower { get; set; }

        // Foreign Key to the followed user
        [ForeignKey(nameof(Followed))]
        public long FollowedId { get; set; }
        public User Followed { get; set; }
    }

}
