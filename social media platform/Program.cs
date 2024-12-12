using social_media_platform.data;
using social_media_platform.models;
namespace social_media_platform
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Userfeatures userfeatures = new Userfeatures();
            User user =userfeatures.login();
            PostServices postservices = new PostServices();
            postservices.addPost(user);
            Console.WriteLine("enter post id");
            long id = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine(postservices.editPost(user, id));
            Console.WriteLine(postservices.removePost(user, id));

        }
    }
}
