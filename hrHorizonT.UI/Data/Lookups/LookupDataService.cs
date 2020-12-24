using hrHorizonT.DataAccess;
using hrHorizonT.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hrHorizonT.UI.Data.Lookups
{
    public class LookupDataService : IFriendLookupDataService
    {
        private Func<hrHorizonTDbContext> _contexyCreator;

        public LookupDataService(Func<hrHorizonTDbContext> contextCreator)
        {
            _contexyCreator = contextCreator;
        }

        public async Task<IEnumerable<LookupItem>> GetFriendLookupAsync()
        {
            using (var ctx = _contexyCreator())
            {
                return await ctx.Friends.AsNoTracking().Select(f => new LookupItem
                {
                    Id = f.Id, 
                    DisplayMember = f.FirstName + " " + f.LastName

                }).ToListAsync();
            }
        }
          
        public async Task<IEnumerable<LookupItem>> GetProgramingLanguageLookupAsync()
        {
            using (var ctx = _contexyCreator())
            {
                return await ctx.ProgrammingLanguages.AsNoTracking().Select(f => new LookupItem
                {
                    Id = f.Id,
                    DisplayMember = f.Name

                }).ToListAsync();
            }
        }
    }

}
