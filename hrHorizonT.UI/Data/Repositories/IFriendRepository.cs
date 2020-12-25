using hrHorizonT.Model;
using System.Collections.Generic;

namespace hrHorizonT.UI.Data.Repositories
{
    public interface IFriendRepository : IGenericRepository<Friend>
    {
        void RemovePhoneNumber(FriendPhoneNumber model);
    }
}