using social_media_platform.data;
using social_media_platform.models;
namespace social_media_platform
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Userfeatures userfeatures = new Userfeatures();
            userfeatures.SignUp();
            User user =userfeatures.login();
        }
    }
}
