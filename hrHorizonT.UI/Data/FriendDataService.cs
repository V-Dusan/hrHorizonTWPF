using hrHorizonT.Model;
using System.Collections.Generic;

namespace hrHorizonT.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        public IEnumerable<Friend> GetAll()
        {
            //TODO: Load data from real service
            yield return new Friend { FirstName = "Dusan", LastName = "Vasilijevic" };
            yield return new Friend { FirstName = "Milun", LastName = "Vasilijevic" };
            yield return new Friend { FirstName = "Katarina", LastName = "Vasilijevic" };
            yield return new Friend { FirstName = "Sanja", LastName = "Vasilijevic" };
        }
    }
}
