using hrHorizonT.DataAccess;
using hrHorizonT.Model;

namespace hrHorizonT.UI.Data.Repositories
{
    public class ProgrammingLanguageRepository : GenericRepository<ProgrammingLanguage, hrHorizonTDbContext>, IProgrammingLanguageRepository
    {
        public ProgrammingLanguageRepository(hrHorizonTDbContext context) : base(context)
        {

        }
    }
}
