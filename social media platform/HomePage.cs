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
        private List<long> UsersIds=new List<long>();
        private int currentUsersIndex=0;
        private int currentPostIndex=0;
        private Deque<Post> postsDeque=new Deque<Post>();
        private List<Comment> comments;
        private bool morePosts= true;
        public HomePage(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        private void GetListOfFollowedUsers(User user) 
        {
               UsersIds = _appDbContext.followedUsers
                .Where(f => f.FollowerId == user.UserId)
                .Select(f => f.FollowedId)
                .ToList();
        }
        private void PostsAddFirst()
        {
         var post = _appDbContext.posts
         .Where(u => u.UserId == UsersIds[currentUsersIndex])
         .FirstOrDefault();

            if (post != null)
            {
                postsDeque.AddToFront(post);
            }
        }
        private void PostsAddLast()
        {
            var post = _appDbContext.posts
            .Where(u => u.UserId == UsersIds[currentUsersIndex])
            .FirstOrDefault();

            if (post != null)
            {
                postsDeque.AddToBack(post);
            }
        }
        private void CallFivePosts(Action act, User user)
        {
            for(int i = 0; i < 5; i++)
            {
                if (UsersIds.Count > currentUsersIndex)
                {
                    act();
                    currentUsersIndex++;
                }
            }
        }
        public void PreparePosts(User user)
        {
            GetListOfFollowedUsers(user);
            CallFivePosts(PostsAddFirst,user);
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
        public void showPreviousPost(User user, out long postId)
        {
            if (currentPostIndex==0)
            {
                Post post = postsDeque[currentPostIndex];
                CallFivePosts(PostsAddFirst, user);
                currentPostIndex=postsDeque.IndexOf(post);
            }
            if (currentPostIndex==0)
            {
                Console.WriteLine("sorry there is no more previous posts");
                postId = postsDeque[++currentPostIndex].PostId;
                currentPostIndex--;
                return;
            }
            else
            {
                showPost(postsDeque[--currentPostIndex]);
                postId = postsDeque[currentPostIndex].PostId;
                return;
            }

        }
        public void showNextPost(User user, out long postId)
        {
            if (postsDeque.Count ==currentPostIndex)
            {
                CallFivePosts(PostsAddLast, user);
            }
            if (postsDeque.Count == currentPostIndex)
            {
                Console.WriteLine("sorry there is no more Next posts");
                postId = postsDeque[--currentPostIndex].PostId;
                currentPostIndex++;
                return;
            }
            else
            {
                showPost(postsDeque[currentPostIndex]);
                postId = postsDeque[currentPostIndex].PostId;
                currentPostIndex++;
                return;
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
