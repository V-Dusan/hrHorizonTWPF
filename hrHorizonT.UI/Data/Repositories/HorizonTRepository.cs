using hrHorizonT.DataAccess;
using hrHorizonT.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace hrHorizonT.UI.Data.Repositories
{
    public class HorizonTRepository : GenericRepository<Friend,hrHorizonTDbContext>, IHorizonTRepository
    {

        public HorizonTRepository(hrHorizonTDbContext context) : base(context)
        {
        }

        public override async Task<Friend> GetByIdAsync(int friendId)
        {
            return await Context.Friends
                .Include(f => f.PhoneNumbers)
                .SingleAsync(f => f.Id == friendId);
        }

        public void RemovePhoneNumber(FriendPhoneNumber model)
        {
            Context.FriendPhoneNumbers.Remove(model);
        }
    }

}
