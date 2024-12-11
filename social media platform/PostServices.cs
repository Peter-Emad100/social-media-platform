using Microsoft.IdentityModel.Tokens;
using social_media_platform.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using social_media_platform.models;
namespace social_media_platform
{
    internal class PostServices
    {
        public bool addPost(User user)
        {
            var context = new AppDbContext();
            string content;
            do {
                Console.WriteLine("What's on your mind ?");
                content = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(content));
            using(context){
                Post post = new Post() { UserId = user.UserId, Content = content, FirstName = user.FirstName, LastName = user.LastName, PostCreationTime = DateTimeOffset.UtcNow };
                context.Add(post);
                context.SaveChanges();
            }
            return true;
        }
    }
}
