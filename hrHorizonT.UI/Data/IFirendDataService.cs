using hrHorizonT.Model;
using System.Collections.Generic;

namespace hrHorizonT.UI.Data
{
    public interface IFriendDataService
    {
        IEnumerable<Friend> GetAll();
    }
}