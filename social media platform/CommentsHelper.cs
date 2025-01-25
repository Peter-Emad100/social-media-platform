using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace social_media_platform
{
    internal class CommentsHelper
    {
        public const int nextComment = 0;
        public const int changeComment = 1;
        public const int deleteComment = 2;
        public const int backToPosts = 3;
        public const int lastOption = 3; // should be changed when adding new feature;
    }
}
