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
            long currentPostID;
            int choice =homepage.showMultiPosts(user,out currentPostID);
            FollowService followService=new FollowService(dbContext);
            PostServices postServices = new PostServices(dbContext);
            ReactService reactService = new ReactService(dbContext);
            CommentService commentService = new CommentService(dbContext);
            while (true)
            {
                if (choice == Helper.nextpostNum || choice == Helper.previousPostNum)
                {
                    homepage.showMultiPosts(user,out currentPostID);
                }
                else if(choice == Helper.unfollowNum)
                {
                    if (followService.unfollow(user, currentPostID))
                        Console.WriteLine("you unfollowed this user");
                    else
                        Console.WriteLine("you already unfollowed this user before");
                    Console.WriteLine("what do you want to do next ?");
                    choice = homepage.takeUserChoice();
                }
                else if(choice == Helper.reactNum)
                {
                    reactService.AddReact(user, currentPostID);

                    choice = homepage.takeUserChoice();
                }
                else if (choice == Helper.WriteCommentNum)
                {
                    commentService.CreateComment(user, currentPostID);

                    choice= homepage.takeUserChoice();
                }
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
