using Birthday.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Data_Access
{
    internal interface IBirthdayDataAccess
    {
        IEnumerable<BirthdayEntry> GetAllBirthdays();
        IEnumerable<BirthdayEntry> GetUpcomingBirthdays();
        void AddEntry(BirthdayEntry entry);
        void RemoveEntry(int entryId);
        void UpdateEntry(BirthdayEntry updateEntry);
    }
}
