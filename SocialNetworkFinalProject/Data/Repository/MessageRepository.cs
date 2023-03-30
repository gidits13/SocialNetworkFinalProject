using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialNetworkFinalProject.Models;

namespace SocialNetworkFinalProject.Data.Repository
{
    public class MessageRepository : Repository<Message>
    {
        public MessageRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task <List<Message>> GetMessages(User sender, User recipient)
        {
            Set.Include(x => x.Recipient);
            Set.Include(x => x.Sender);

            var from = await Set.Where(x => x.SenderId == sender.Id && x.RecipientId == recipient.Id).ToListAsync();
            var to = await Set.Where(x => x.SenderId == recipient.Id && x.RecipientId == sender.Id).ToListAsync();
            /*            var from = Set.AsEnumerable().Where(x => x.SenderId == sender.Id && x.RecipientId==recipient.Id).ToList();
                        var to = Set.AsEnumerable().Where(x => x.SenderId == recipient.Id && x.RecipientId == sender.Id).ToList();*/

            var list = new List<Message>();
            list.AddRange(from);
            list.AddRange(to);
            list=list.OrderBy(x => x.Id).ToList();
            return list;
        }
    }
}
