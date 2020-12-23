using hrHorizonT.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hrHorizonT.UI.Data.Repositories
{
    public interface IHorizonTRepository
    {
        Task<Friend> GetByIdAsync(int friendId);
        Task SaveAsync();
        bool HasChanges();
    }
}