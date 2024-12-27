using social_media_platform.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using social_media_platform.models;
namespace social_media_platform
{
    internal class FollowService
    {
        private AppDbContext _appDbContext;
        public FollowService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public bool follow(User user , long postUserId)
        {
            FollowedUser followedUser = new FollowedUser()
            {
                FollowerId = user.UserId,
                FollowedId = postUserId
            };
            try
            {
                _appDbContext.Add(followedUser);
                _appDbContext.SaveChanges();
            }catch (Exception ex)
            {
                Console.WriteLine("following this user is currently unavaiable");
                return false;
            }
            return true;
        }
        public bool unfollow(User user, long postUserId)
        {
            var followedUser = _appDbContext.followedUsers.SingleOrDefault(f => f.FollowedId == postUserId && f.FollowerId==user.UserId);
            if(followedUser != null)
            {
                 _appDbContext.Remove(followedUser);
                _appDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
