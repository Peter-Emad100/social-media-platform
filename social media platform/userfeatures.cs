using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using social_media_platform.data;
namespace social_media_platform
{
    using BCrypt.Net;

    internal class userfeatures
    {
        public static bool checkEmail(String email)
        {
            var context = new AppDbContext();
            if(string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            try
            {
                var mail = new MailAddress(email);
            }
            catch (Exception ex)
            {
                return false;
            }
            bool found = context.users.Any(u=>u.Email == email);
            if (found)
            {
                Console.WriteLine("This Email is Already Used");
                return false;
            }
            return true;
            
        }
        public static void SignUp()
        {
            var context = new AppDbContext();
            Console.WriteLine("Welcom to Sign UP");
            String firstName;
            String lastName;
            String email;
            String password;
            do
            {
                Console.WriteLine("Please Enter your First name");
                firstName = Console.ReadLine();
            } while (String.IsNullOrWhiteSpace(firstName));
            do
            {
                Console.WriteLine("Please Enter your Last name");
                 lastName = Console.ReadLine();
            } while (String.IsNullOrWhiteSpace(lastName));
            do
            {
                Console.WriteLine("Please Enter your Email");
                email = Console.ReadLine();

            } while (! checkEmail(email));

            do
            {
                Console.WriteLine("Please Enter 8 letters to 20 letters Password");
                password = Console.ReadLine();
            } while (String.IsNullOrWhiteSpace(password) && password.Length > 7 && password.Length <21);

            password = BCrypt.EnhancedHashPassword(password);
            
        }
    }
}
