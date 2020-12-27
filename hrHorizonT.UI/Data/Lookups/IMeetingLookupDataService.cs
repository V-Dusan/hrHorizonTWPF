using hrHorizonT.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hrHorizonT.UI.Data.Lookups
{
    public interface IMeetingLookupDataService
    {
        Task<List<LookupItem>> GetMeetingLookupAsync();
    }
}
