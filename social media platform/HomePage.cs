using Nito.Collections;
using social_media_platform.data;
using social_media_platform.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        private List<Comment> comments;
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
        public int takeUserChoice()
        {
            Console.WriteLine($@"press {PostsHelper.nextpostNum} for next post
                press {PostsHelper.previousPostNum} for the previous post
                press {PostsHelper.showCommentsNum} for  watching comments
                press {PostsHelper.unfollowNum} for unfollow
                press {PostsHelper.reactNum} for react
                press {PostsHelper.WriteCommentNum} for writing your own comment");
            // if you added new feature you should increase lastOptionChoice in helper
            int y;
            if( int.TryParse(Console.ReadLine(),out y))
            {
                if(y >0 && y <=PostsHelper.lastOptionChoice)
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
        public void showMultiPosts(User userm , out long postId , int choice)
        {
            if (choice == PostsHelper.previousPostNum &&
                postsDeque.First().Equals(postsDeque[currentPostIndex]))
            {
                Post post = postsDeque[currentPostIndex];
                CallFivePosts(PostsAddFirst);
                currentPostIndex = postsDeque.IndexOf(post) - 1;
            }
            else if (choice == PostsHelper.previousPostNum)
            {
                currentPostIndex--;
            }
            else if (choice == PostsHelper.nextpostNum &&
                 postsDeque.Last().Equals(postsDeque[currentPostIndex]))
            {
                CallFivePosts(PostsAddLast);
                currentPostIndex++;
            }
            else if (choice == PostsHelper.nextpostNum)
            {
                currentPostIndex++;
            }
            if (currentPostIndex < postsDeque.Count())
            {
                showPost(postsDeque[currentPostIndex]);
                postId = postsDeque[currentPostIndex].PostId;
            }
            else
            {
                Console.WriteLine("that all you have for today");
                Thread.Sleep(5000);
                Process.GetCurrentProcess().Kill();
                //logically useless just for the compiler
                postId = 0;
            }
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
        public int takeUserCommentChoice()
        {
            Console.WriteLine(@$"{CommentsHelper.nextComment} for next comment 
                {CommentsHelper.changeComment} for change comment (if it is yours)
                {CommentsHelper.deleteComment} for delete comment (if it is yours)
                {CommentsHelper.backToPosts} back to posts");
            int choice;
            if(int.TryParse(Console.ReadLine(), out choice)&&choice >= 0 && choice <= CommentsHelper.lastOption)
            {
                return choice;
            }
            else
            {
                return takeUserCommentChoice();
            }

        }
        public void ShowMultiComments(User user, long postId)
        {
            GetListOfComments(postId);
            int x = 0;
            bool stayIn = true;
            CommentService commentService = new CommentService(_appDbContext);
            while (comments.Count() > x && stayIn)
            {
                ShowComment(comments.ElementAt(x));
                int choice = takeUserCommentChoice();
                
                switch (choice)
                {
                    case CommentsHelper.nextComment:
                        x++;
                        break;
                    case CommentsHelper.changeComment:
                        commentService.ChangeComment(user, comments.ElementAt(x).CommentId);
                        break;
                    case CommentsHelper.deleteComment:
                        if(commentService.DeleteComment(user, comments.ElementAt(x).CommentId))
                            comments.RemoveAt(x);
                        break;
                    case CommentsHelper.backToPosts:
                        stayIn = false;
                        break;
                    default:
                        Console.WriteLine("unexpected stuff happend, try again");
                        break;
                }
            }
        }

        private void GetListOfComments(long postId)
        {
            comments= new List<Comment>();
            comments=_appDbContext.comments.Where(c=>c.PostId == postId).ToList();
        }
    }
}
