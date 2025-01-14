using social_media_platform.data;
using social_media_platform.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace social_media_platform
{
    internal class HomePage
    {
        internal AppDbContext _appDbContext;
        public HomePage(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        List<long> followedUsersIds;
        private List<long> GetListOfFollowedUsers(User user)
        {
            var followedUsers = _appDbContext.followedUsers
                .Where(f => f.FollowerId == user.UserId)
                .Select(f => f.FollowedId)
                .ToList();
            return followedUsers;
        }
        public bool showMultiPosts(User user)
        {
            followedUsersIds ??= GetListOfFollowedUsers(user);
            int i = followedUsersIds.Count();
            long postId;
            while (i > 0)
            {
                showPost(i, out postId);
                i--;
            }
            return true;

        }
        public int showPost(long FollowedUserId, out long postId)
        {

            var post = _appDbContext.posts.LastOrDefault(p => p.UserId == FollowedUserId);
            Console.WriteLine($"{post.FirstName} {post.LastName} \n {post.Content} " +
                $"\n  comments: \n {post.Comments}");
            postId = post.PostId;
            return 1;
        }
        public void ShowMultiComments(List<Comment> comments)
        {
            if (comments.Count() > 0)
            {
                for (int i = 0; i < comments.Count; i++)
                {
                    long commentId =ShowComment(comments[i]);
                    Console.WriteLine("0 for next comment \n  1 for change comment (if it is yours)" +
                        "2 for delete comment (if it is yours)");
                    int choice;
                    bool sucess =int.TryParse(Console.ReadLine(),out choice);
                    if (sucess)
                    {
                        if (choice == 0)
                        {
                            continue;
                        }
                        else if (choice == 1){

                        }
                        else if (choice == 2)
                        {

                        }
                        else
                        {
                            Console.WriteLine("sorry you input was incorrect")
                        }


                    }
                    else
                    {
                        Console.WriteLine("sorry you input was incorrect");
                    }
                }
            }
            else
            {
                Console.WriteLine("there is no comments on this post, be the first and leave your comment");
            }
        }
        public long ShowComment(Comment comment)
        {
            try
            {
                Console.WriteLine($"{comment.FirstName} {comment.LastName} \n " +
                    $"{comment.Content}\n {comment.CommentCreationTime}");
                return comment.CommentId;
            }
            catch { return -1; }
        }
    }
}
