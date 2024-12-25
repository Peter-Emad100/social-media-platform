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
            userfeatures.SignUp();
            User user =userfeatures.login();
            ReactService reactService = new ReactService(serviceProvider.GetRequiredService<AppDbContext>());
            reactService.AddReact(user, 1);

        }
    }
}
