using social_media_platform.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using social_media_platform.models;

namespace social_media_platform
{
    internal class CommentService
    {
         internal AppDbContext _appDbContext;
        public CommentService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public bool CreateComment(User user, long post_id)
        {
            string content;
            do
            {
                Console.WriteLine("Enter your comment");
                content = Console.ReadLine();
            } while (String.IsNullOrEmpty(content));
            Comment comment = new Comment() { Content = content,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PostId = post_id,
                UserId = user.UserId,
                CommentCreationTime = DateTimeOffset.UtcNow
            };
            _appDbContext.Add(comment);
            _appDbContext.SaveChanges();
            return true;
        }
        public bool DeleteComment(User user,long commentId)
        {
            Comment comment = _appDbContext.comments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment == null) return false;
            if (user.UserId != comment.UserId) return false;
            else
            {
                _appDbContext.comments.Remove(comment);
                _appDbContext.SaveChanges();
                return true;
            }
        }
        public bool ChangeComment(User user,long commentId)
        {
            Comment comment= _appDbContext.comments.FirstOrDefault(c=> c.CommentId == commentId);
            if(comment == null) return false;
            if (user.UserId != comment.UserId) return false;
            else
            {
                String content;
                Console.WriteLine("Enter your new comment");
                content= Console.ReadLine();
                if(String.IsNullOrEmpty(content)) return false;
                else
                {
                    comment.Content = content;
                    _appDbContext.SaveChanges();
                    return true;
                }
            }
        }
    }
}
