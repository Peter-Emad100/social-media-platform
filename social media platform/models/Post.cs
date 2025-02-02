﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace social_media_platform.models
{
    internal class Post
    {

        public long PostId { get; set; }
        public long UserId { get; set; }
        public string Content { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTimeOffset PostCreationTime { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public ICollection<ReactLog> ReactLogs{ get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Post post)
            {
                return this.PostId == post.PostId;
            }
            return false;
        }
    }
}
