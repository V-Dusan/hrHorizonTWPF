using hrHorizonT.DataAccess;
using hrHorizonT.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace hrHorizonT.UI.Data.Repositories
{
    public class DrzavaRepository : GenericRepository<Drzava, hrHorizonTDbContext>, IDrzavaRepository
    {
        public DrzavaRepository(hrHorizonTDbContext context) : base(context)
        {
        }

        
    }
}
