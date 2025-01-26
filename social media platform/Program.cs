using social_media_platform.data;
using social_media_platform.models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace social_media_platform
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            var serviceProvider = InitializeServices();
            var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
            Userfeatures userfeatures = new Userfeatures(dbContext);
            User user = userfeatures.login();
            HomePage homepage = new HomePage(dbContext);
            homepage.PreparePosts(user);
            long currentPostID=0;
            int choice = PostsHelper.nextpostNum;
            
            PostServices postServices = new PostServices(dbContext);
            
            CommentService commentService = new CommentService(dbContext);
            while (true)
            {
                switch (choice)
                {
                    case PostsHelper.previousPostNum:
                        homepage.showPreviousPost(user, out currentPostID);
                        break;
                    case PostsHelper.nextpostNum:
                        homepage.showNextPost(user, out currentPostID);
                        break;
                    case PostsHelper.showCommentsNum:
                        homepage.ShowMultiComments(user, currentPostID);
                        break;
                    case PostsHelper.unfollowNum:
                        FollowService followService = new FollowService(dbContext);
                        if (followService.unfollow(user, currentPostID))
                            Console.WriteLine("you unfollowed this user");
                        else
                            Console.WriteLine("you already unfollowed this user before");
                        Console.WriteLine("what do you want to do next ?");
                        break;
                    case PostsHelper.reactNum:
                        ReactService reactService = new ReactService(dbContext);
                        reactService.AddReact(user, currentPostID);
                        break;
                    case PostsHelper.WriteCommentNum:
                        commentService.CreateComment(user, currentPostID);
                        break;
                    default:
                        Console.WriteLine("invalid choice");
                        break;
                }
                choice = homepage.takeUserChoice();
            }
        }
        static ServiceProvider InitializeServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>
                (options => options.UseSqlServer("Server=localhost;Database=SocialMediaDb;Integrated Security=true;TrustServerCertificate=True;"));
            services.AddScoped<CommentService>();
            services.AddScoped<FollowService>();
            services.AddScoped<HomePage>();
            services.AddScoped<PostServices>();
            services.AddScoped<ReactService>();
            services.AddScoped<Userfeatures>();
            return services.BuildServiceProvider();
        }
    }
}
