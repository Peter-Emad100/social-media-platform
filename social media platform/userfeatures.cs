﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using social_media_platform.data;
using social_media_platform.models;
namespace social_media_platform
{
    using BCrypt.Net;
    using System.ComponentModel;
    using static System.Net.Mime.MediaTypeNames;

    internal class userfeatures
    {
        public static bool checkEmail(String email)
        {
          
            if (string.IsNullOrWhiteSpace(email))
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

            } while (!checkEmail(email) || context.users.Any(u => u.Email == email));

            do
            {
                Console.WriteLine("Please Enter 8 letters to 20 letters Password");
                password = Console.ReadLine();
            } while (!checkPassword(password));

            password = BCrypt.EnhancedHashPassword(password);

            using (context)
            {
                User user = new User() { FirstName= firstName, LastName = lastName, Email = email , HashedPassword=password};
                context.users.Add(user);
                context.SaveChanges();
            }
        }
        public static bool checkPassword(String password)
        {
            return !String.IsNullOrWhiteSpace(password) && password.Length > 7 && password.Length < 21;
        }
        public static long login()
        {
            using (var context = new AppDbContext()) {
                long user_id = 0;
                do
                {
                    Console.WriteLine("Enter your Email");
                    string email = Console.ReadLine();
                    Console.WriteLine("Enter you password");
                    string password = Console.ReadLine();
                    if (!checkEmail(email) || !checkPassword(password)) {
                        Console.WriteLine("Wrong email or password format");
                        continue;
                    }
                    User user = context.users.FirstOrDefault(u => u.Email == email);
                    if (user != null && BCrypt.EnhancedVerify(password, user.HashedPassword))
                    {
                        user_id = user.UserId;
                        return user_id;
                    }
                    else
                    {
                        Console.WriteLine("Wrong email or password. Try again.");
                    }

                } while(true);
        } }
    }
}
