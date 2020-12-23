using hrHorizonT.DataAccess;
using hrHorizonT.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hrHorizonT.UI.Data.Repositories
{
    public class HorizonTRepository : IHorizonTRepository
    {
        private hrHorizonTDbContext _context;

        public HorizonTRepository(hrHorizonTDbContext context)
        {
            _context = context;
        }

        public async Task<Friend> GetByIdAsync(int friendId)
        {
            return await _context.Friends.SingleAsync(f => f.Id == friendId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
