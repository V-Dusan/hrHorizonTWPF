using hrHorizonT.DataAccess;
using hrHorizonT.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace hrHorizonT.UI.Data.Repositories
{
    public class MeetingRepository : GenericRepository<Meeting, hrHorizonTDbContext>, IMeetingRepository
    {
        public MeetingRepository(hrHorizonTDbContext context) : base(context)
        {

        }

        public async override Task<Meeting> GetByIdAsync(int id)
        {
            return await Context.Meetings.Include(m => m.Friends).SingleAsync(m => m.Id == id);
        }
    }
}
