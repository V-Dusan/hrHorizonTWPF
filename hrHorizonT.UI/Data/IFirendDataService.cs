using hrHorizonT.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hrHorizonT.UI.Data
{
    public interface IFriendDataService
    {
        Task<Friend> GetByIdAsync(int friendId);
        Task SaveAsync(Friend friend);
    }
}