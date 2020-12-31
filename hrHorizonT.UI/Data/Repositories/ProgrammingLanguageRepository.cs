using hrHorizonT.DataAccess;
using hrHorizonT.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace hrHorizonT.UI.Data.Repositories
{
    public class ProgrammingLanguageRepository : GenericRepository<ProgrammingLanguage, hrHorizonTDbContext>, IProgrammingLanguageRepository
    {
        public ProgrammingLanguageRepository(hrHorizonTDbContext context) : base(context)
        {

        }
    }
}
