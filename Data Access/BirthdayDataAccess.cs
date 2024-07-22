using Birthday.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Data_Access
{
    internal class BirthdayDataAccess : IBirthdayDataAccess
    {
        private readonly BirthdayDbContext dbContext;
        public BirthdayDataAccess(BirthdayDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<BirthdayEntry> GetAllBirthdays()
        {
            return dbContext.BirthdayEntries.ToList();
        }
/*        public IEnumerable<BirthdayEntry> GetUpcomingBirthdays(int countDays)
        {
            DateTime currentDate = DateTime.Today;
            DateTime endDate = currentDate.AddDays(countDays);
            return dbContext.BirthdayEntries.Where(x => x.DateOfBirth >= currentDate && x.DateOfBirth <= endDate).ToList();
        }*/
        public void AddEntry(BirthdayEntry entry)
        {
            dbContext.BirthdayEntries.Add(entry);
            dbContext.SaveChanges();
        }
        public void RemoveEntry(int entryId) 
        {
            var entryToRemove = dbContext.BirthdayEntries.Find(entryId);
            if (entryToRemove != null)
            {
                dbContext.BirthdayEntries.Remove(entryToRemove);
                dbContext.SaveChanges();
            }
        }
        public void UpdateEntry(BirthdayEntry updateEntry)
        {
            var entryToUpdate = dbContext.BirthdayEntries.Find(updateEntry.Id);
            if (entryToUpdate != null)
            {
                entryToUpdate.Name = updateEntry.Name;
                entryToUpdate.DateOfBirth = updateEntry.DateOfBirth;
                dbContext.SaveChanges();
            }
        }
    }
}
