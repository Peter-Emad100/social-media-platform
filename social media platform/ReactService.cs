using social_media_platform.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using social_media_platform.models;
namespace social_media_platform
{
    internal class ReactService
    {
        AppDbContext _appDbContext;
        public ReactService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public bool AddReact(User user , long postId)
        {
            ReactsEnum reactsEnum = new ReactsEnum();
            do
            {
                Console.WriteLine("choose your react \n 0 => Love \n" +
                    " 1 => Gem \n 2 => Haha \n 3 => warning \n 4 =>Sad \n 5 => Angry");
                if (int.TryParse(Console.ReadLine(), out int intChoice) &&
                            Enum.IsDefined(typeof(ReactsEnum), intChoice))
                {
                    reactsEnum = (ReactsEnum)intChoice;
                    ReactLog log = new ReactLog() { FirstName = user.FirstName, LastName = user.LastName, UserId=user.UserId, PostId=postId, React=reactsEnum};
                    _appDbContext.Add(log);
                    _appDbContext.SaveChanges();
                    Console.WriteLine($"Your {reactsEnum} has been saved");
                    break;
                }
                } while (true);
            return true;
        }
        public bool RemoveReact(User user , long postId) {
            ReactLog reactLog = _appDbContext.reactLogs.SingleOrDefault(r => r.UserId==user.UserId && r.PostId==postId);
            if (reactLog != null)
            {
                _appDbContext.Remove(reactLog);
                _appDbContext.SaveChanges();
                Console.WriteLine("Your react has been deleted");
                return true;
            }
            return false;
        }
        public bool editReact(User user, long postId)
        {
            ReactLog reactLog=_appDbContext.reactLogs.SingleOrDefault(r=> r.PostId == postId && r.UserId==user.UserId);
            if (reactLog != null)
            {
                do
                {
                    Console.WriteLine("what do you want to change your react to ?");
                    Console.WriteLine("choose your react \n 0 => Love \n" +
                        " 1 => Gem \n 2 => Haha \n 3 => warning \n 4 =>Sad \n 5 => Angry");
                    ReactsEnum reactenum = new ReactsEnum();
                    if (int.TryParse(Console.ReadLine(), out int reactInt) &&
                        Enum.IsDefined(reactenum.GetType(), reactInt))
                    {
                        reactenum = (ReactsEnum)reactInt;
                        reactLog.React = reactenum;
                        _appDbContext.SaveChanges();
                        Console.WriteLine("your react has been changed");
                        return true;
                    }
                } while (true);
            }
            return false;
        }
    }
}
