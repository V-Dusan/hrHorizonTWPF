using hrHorizonT.Model;
using System.Threading.Tasks;

namespace hrHorizonT.UI.Data.Repositories
{
    public interface IProgrammingLanguageRepository : IGenericRepository<ProgrammingLanguage>
    {
        Task<bool> IsReferencedByFriendAsync(int programmingLanguageId);
    }
}