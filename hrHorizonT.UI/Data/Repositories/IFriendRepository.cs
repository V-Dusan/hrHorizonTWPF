using hrHorizonT.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hrHorizonT.UI.Data.Repositories
{
    public interface IFriendRepository : IGenericRepository<Friend>
    {
        void RemovePhoneNumber(FriendPhoneNumber model);
        Task<bool> HasMeetingAsync(int friendid);
    }
}