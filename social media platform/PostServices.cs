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
        internal AppDbContext _appDbContext;
        public PostServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Post addPost(User user)
        {
            string content;
            Post post;
            do {
                Console.WriteLine("What's on your mind ?");
                content = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(content));
                post = new Post() { UserId = user.UserId, Content = content, FirstName = user.FirstName, LastName = user.LastName, PostCreationTime = DateTimeOffset.UtcNow };
                _appDbContext.Add(post);
                _appDbContext.SaveChanges();
            
            return post;
        }
        public bool removePost(User user,long id) {
            if (user == null || id<= 0) return false;
                Post post = _appDbContext.posts.FirstOrDefault(p => p.PostId == id);
                if (post == null) return false;
                if (post.UserId == user.UserId)
                {
                    _appDbContext.Remove(post);
                    _appDbContext.SaveChanges();
                    return true;
                }
                return false;
            
        }
        public bool editPost(User user, long id)
        {
            if (user == null || id <= 0) { return false; }
                Post post = _appDbContext.posts.FirstOrDefault(p => p.PostId == id);
                if (post == null) return false;
                String content;
                if (post.UserId == user.UserId)
                {
                    Console.WriteLine("Enter your new post content");
                    content = Console.ReadLine();
                    if (string.IsNullOrEmpty(content)) return false;
                    post.Content = content;
                    _appDbContext.SaveChanges();
                    return true;
                
            }
            return false;
        }
    }
}
