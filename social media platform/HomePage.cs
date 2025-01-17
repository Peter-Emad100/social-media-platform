using Nito.Collections;
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
        private List<long> followedUsersIds=new List<long>();
        private int current=0;
        private Deque<Post> posts=new Deque<Post>();
        public HomePage(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        private void GetListOfFollowedUsers(User user)
        {
               followedUsersIds = _appDbContext.followedUsers
                .Where(f => f.FollowerId == user.UserId)
                .Select(f => f.FollowedId)
                .ToList();
        }
        private void PostsAddFirst()
        {
            posts.AddToFront(_appDbContext.posts.Where(u => u.UserId == followedUsersIds[current]).FirstOrDefault());
        }
        private void PostsAddLast()
        {
            posts.AddToBack(_appDbContext.posts.Where(u => u.UserId == followedUsersIds[current]).FirstOrDefault());
        }
        private void CallFivePosts(Action act)
        {
            for(int i = 0; i < 5; i++)
            {
                if (followedUsersIds.Count > current)
                {
                    act();
                    current++;
                }
                /*
                if (posts.First() == null)
                {
                    posts.RemoveFirst();
                }
                if (posts.Last() == null)
                {
                    posts.RemoveLast();
                }*/
            }
        }
        private bool showPost(Post posty)
        {
            Console.WriteLine($"{posty.FirstName} {posty.LastName} \n {posty.Content} " +
                $"\n  comments: \n {posty.Comments}");
            return true;
        }
        public bool showMultiPosts(User user)
        {
            GetListOfFollowedUsers(user);
            CallFivePosts(PostsAddFirst);
            foreach (Post posty in posts)
            {
                showPost(posty);
            }
            return true;
        }
        private long ShowComment(Comment comment)
        {
            try
            {
                Console.WriteLine($"{comment.FirstName} {comment.LastName} \n " +
                    $"{comment.Content}\n {comment.CommentCreationTime}");
                return comment.CommentId;
            }
            catch { return -1; }
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
                            Console.WriteLine("sorry you input was incorrect");
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
    }
}
