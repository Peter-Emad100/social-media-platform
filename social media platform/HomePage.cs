using Nito.Collections;
using social_media_platform.data;
using social_media_platform.models;
using System;
using System.Collections;
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
        private int currentFollowedIndex=0;
        private int currentPostIndex=0;
        private Deque<Post> postsDeque=new Deque<Post>();
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
            postsDeque.AddToFront(_appDbContext.posts.Where(u => u.UserId == followedUsersIds[currentFollowedIndex]).FirstOrDefault());
        }
        private void PostsAddLast()
        {
            postsDeque.AddToBack(_appDbContext.posts.Where(u => u.UserId == followedUsersIds[currentFollowedIndex]).FirstOrDefault());
        }
        private void CallFivePosts(Action act)
        {
            for(int i = 0; i < 5; i++)
            {
                if (followedUsersIds.Count > currentFollowedIndex)
                {
                    act();
                    currentFollowedIndex++;
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
        public void PreparePosts(User user)
        {
            GetListOfFollowedUsers(user);
            CallFivePosts(PostsAddFirst);
        }
        private bool showPost(Post posty)
        {
            Console.WriteLine($"{posty.FirstName} {posty.LastName} \n {posty.Content} " +
                $"\n  comments: \n {posty.Comments}");
            return true;
        }
        private int takeUserChoice()
        {
            Console.WriteLine("press 1 for next post \n" +
                "press 2 for  watching comments \n" +
                "press 3 for unfollow \n" +
                "press 4 for react \n" +
                "press 5 for editing your own react if you already reacted\n" +
                "press 5 for writing your own comment\n" +
                "press 6 for unfollow \n"+
                "press 7 for the next post \n"+
                "press 8 for the previous post");
            int y;
            if( int.TryParse(Console.ReadLine(),out y))
            {
                if(y >0 && y < 9)
                {
                    return y;
                }
            }
            /*if he entered the choice wrong function will be fired again
             * ... i think this might be wrong 
             * because of opening func into func in stack frame without closing the first
             * but i will search more*/
            return takeUserChoice();
            
        }
        public int showMultiPosts(User user)
        {
             if (postsDeque.Last().Equals(postsDeque[currentPostIndex]))
            {
                CallFivePosts(PostsAddLast);
            }
            if (currentPostIndex < postsDeque.Count())
            {
                showPost(postsDeque[currentPostIndex]);
                
            }
           int choice= takeUserChoice();
           if (choice == 8 && postsDeque.First().Equals(postsDeque[currentPostIndex])){
                Post post = postsDeque[currentPostIndex];
                CallFivePosts(PostsAddFirst);
                currentPostIndex = postsDeque.IndexOf(post) - 1;
            }
           else if(choice == 8)
            {
                currentPostIndex--;
            }
           else if (choice == 7)
            {
                currentPostIndex++;
            }
            return choice;
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
