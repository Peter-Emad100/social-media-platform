﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace social_media_platform.models
{
    internal class ReactLog
    {
        public long ReactLogId {  get; set; }
        public long UserId { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public byte ReactId {  get; set; }
        public React React { get; set; }
        public long PostId {  get; set; }
        public Post Post { get; set; }

    }
}
