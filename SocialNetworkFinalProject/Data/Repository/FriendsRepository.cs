using Microsoft.EntityFrameworkCore;
using SocialNetworkFinalProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkFinalProject.Data.Repository
{
    public class FriendsRepository : Repository<Friend>
    {
        public FriendsRepository(ApplicationDbContext db) : base(db)
        {
        }
        public async Task AddFriend(User target, User Friend)
        {
            var friends = Set.AsEnumerable().FirstOrDefault(x => x.UserId == target.Id && x.CurrentFriendId == Friend.Id);        

            if(friends is null)
            {
                var item = new Friend()
                {
                    UserId = target.Id,
                    User = target,
                    CurrentFriend = Friend,
                    CurrentFriendId = Friend.Id
                };
                await Create(item);
            }
        }
        /*        public async Task<List<User>> GetFriendByUser(User target)
                {
                    var friends = Set.Include(x => x.CurrentFriend).AsEnumerable().Where(x => x.User?.Id == target.Id).Select(x => x.CurrentFriend);


                    return await Task<List<User>>.Run(() => friends.ToList());
                }*/
        public async Task<List<User>> GetFriendByUser(User target)
        {

            var friends = await Set.Include(x => x.CurrentFriend).Where(x => x.UserId == target.Id).Select(x => x.CurrentFriend).ToListAsync();
            return friends;
        }

        public async Task DeleteFriend(User target, User Friend)
        {
            var friends = Set.AsEnumerable().FirstOrDefault(x=>x.UserId == target.Id && x.CurrentFriendId == Friend.Id);
            await Delete(friends);
        }
    }
}
