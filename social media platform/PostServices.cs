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
        public Post addPost(User user)
        {
            var context = new AppDbContext();
            string content;
            Post post;
            do {
                Console.WriteLine("What's on your mind ?");
                content = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(content));
            using(context){
                post = new Post() { UserId = user.UserId, Content = content, FirstName = user.FirstName, LastName = user.LastName, PostCreationTime = DateTimeOffset.UtcNow };
                context.Add(post);
                context.SaveChanges();
            }
            return post;
        }
        public bool removePost(User user,long id) {
            if (user == null || id<= 0) return false;
            using (var context = new AppDbContext())
            {
                Post post = context.posts.FirstOrDefault(p => p.PostId == id);
                if (post == null) return false;
                if (post.UserId == user.UserId)
                {
                    context.Remove(post);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public bool editPost(User user, long id)
        {
            if (user == null || id <= 0) { return false; }
            using (var context = new AppDbContext())
            {
                Post post = context.posts.FirstOrDefault(p => p.PostId == id);
                if (post == null) return false;
                String content;
                if (post.UserId == user.UserId)
                {
                    Console.WriteLine("Enter your new post content");
                    content = Console.ReadLine();
                    if (string.IsNullOrEmpty(content)) return false;
                    post.Content = content;
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
