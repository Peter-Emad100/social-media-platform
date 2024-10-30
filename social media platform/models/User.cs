using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace social_media_platform.models
{
    internal class User
    {
        public long UserId {  get; set; }
        public string FirstName {  get; set; }
        public string LastName {  get; set; }
        public string HashedPassword {  get; set; }
        public string Email {  get; set; }
    }
}
