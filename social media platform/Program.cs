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
            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Server=localhost;Database=SocialMediaDb;Integrated Security=true;TrustServerCertificate=True;"));
            var serviceProvider= services.BuildServiceProvider();
            Userfeatures userfeatures = new Userfeatures(serviceProvider.GetRequiredService<AppDbContext>());
            User user = userfeatures.login();
            HomePage homepage = new HomePage(serviceProvider.GetRequiredService<AppDbContext>());
            homepage.PreparePosts(user);
            long currentPostID;
            int choice =homepage.showMultiPosts(user,out currentPostID);
            FollowService followService=new FollowService(serviceProvider.GetRequiredService<AppDbContext>());
            PostServices postServices = new PostServices(serviceProvider.GetRequiredService<AppDbContext>());
            ReactService reactService = new ReactService(serviceProvider.GetRequiredService<AppDbContext>());
            CommentService commentService = new CommentService(serviceProvider.GetRequiredService<AppDbContext>());
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
                else if (choice == Helper.WriteCommentNum){
                    commentService.CreateComment(user, currentPostID);
                    choice= homepage.takeUserChoice();
                }
            }
        }
    }
}
