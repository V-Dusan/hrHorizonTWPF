using hrHorizonT.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hrHorizonT.UI.Data.Lookups
{
    public interface IFriendLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetFriendLookupAsync();
    }
}