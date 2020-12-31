using hrHorizonT.DataAccess;
using hrHorizonT.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace hrHorizonT.UI.Data.Repositories
{
    public class ProgrammingLanguageRepository : GenericRepository<ProgrammingLanguage, hrHorizonTDbContext>, IProgrammingLanguageRepository
    {
        public ProgrammingLanguageRepository(hrHorizonTDbContext context) : base(context)
        {

        }

        public async Task<bool> IsReferencedByFriendAsync(int programmingLanguageId)
        {
            return await Context.Friends.AsNoTracking()
                .AnyAsync(f => f.FavoriteLanguageId == programmingLanguageId);
        }
    }
}
