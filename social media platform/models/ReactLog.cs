using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace social_media_platform.models
{
    public enum ReactsEnum
    {
        Love = 0,
        Gem = 1,
        Haha = 2,
        Warning = 3,
        Sad = 4,
        Angry = 5
    }
    internal class ReactLog
    {
        public long UserId { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public ReactsEnum React { get; set; }
        public long PostId {  get; set; }
        public Post Post { get; set; }

    }
}
