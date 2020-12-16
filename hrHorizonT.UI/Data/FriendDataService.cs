using hrHorizonT.DataAccess;
using hrHorizonT.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hrHorizonT.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        private Func<hrHorizonTDbContext> _contextCreator;

        public FriendDataService(Func<hrHorizonTDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<Friend> GetByIdAsync(int friendId)
        {
            using (var ctx = _contextCreator()) 
            {
                return await ctx.Friends.SingleAsync(f => f.Id == friendId);
            }
        }
    }

}
