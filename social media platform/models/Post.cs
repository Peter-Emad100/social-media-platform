using System;
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
        public long UserId {  get; set; }
        public string Content {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }

        public DateTime PostCreationTime {  get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
