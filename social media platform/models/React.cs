using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace social_media_platform.models
{
    public enum ReactsEnum
    {
        Love,
        Angry,
        Haha,
        Sad,
        Warning,
        Gem
    }
    internal class React
    {
        public byte ReactId {  get; set; }
        public ReactsEnum ReactName  { get; set; }
        public ICollection<ReactLog> ReactLogs { get; set; }

    }
}
