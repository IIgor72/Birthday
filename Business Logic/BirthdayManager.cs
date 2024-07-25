using Birthday.Data_Access;
using Birthday.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Business_Logic
{
    internal class BirthdayManager
    {
        private readonly IBirthdayDataAccess birthdayDataAccess;

        public BirthdayManager(IBirthdayDataAccess birthdayDataAccess)
        {
           this.birthdayDataAccess = birthdayDataAccess ?? throw new ArgumentNullException(nameof(birthdayDataAccess));
        }

        public IEnumerable<BirthdayEntry> GetAllBirthdays()
        {
            return birthdayDataAccess.GetAllBirthdays();
        }

        public IEnumerable<BirthdayEntry> GetUpcomingBirthdays(int countDays)
        {
            DateTime currentDate = DateTime.Today;
            DateTime endDate = currentDate.AddDays(countDays);
            return birthdayDataAccess.GetAllBirthdays()
                .Where(x => IsDateInRange(x.DateOfBirth, currentDate, endDate));
        }

        public IEnumerable<BirthdayEntry> GetPastBirthdays()
        {
            DateTime today = DateTime.Today;
            int currentMonth = today.Month;
            int previousMonth = (currentMonth == 1) ? 12 : currentMonth - 1;    // если январь, то ставим 12, иначе текущий - 1

            return birthdayDataAccess.GetAllBirthdays()
                .Where(x => (x.DateOfBirth.Month == previousMonth && x.DateOfBirth.Day >= today.Day) ||     // Прошедшие дни текущего месяца
                            (x.DateOfBirth.Month == currentMonth && x.DateOfBirth.Day <= today.Day))        // Дни текущего месяца
                .OrderBy(y => y.DateOfBirth);
        }

        private bool IsDateInRange(DateTime dateOfBirth, DateTime CurrentDate, DateTime endDate)
        {
            /*Перевод дат в формат (месяц - день) для сравнения*/
            var startMonthDay = new DateTime(1, CurrentDate.Month, CurrentDate.Day);
            var endMonthDay = new DateTime(1, endDate.Month, endDate.Day);
            var birthMonthDay = new DateTime(1, dateOfBirth.Month, dateOfBirth.Day);

            if (startMonthDay <= endMonthDay)
            {
                return birthMonthDay >= startMonthDay && birthMonthDay <= endMonthDay;
            }
            else
            {
                return birthMonthDay >= startMonthDay || birthMonthDay <= endMonthDay;
            }
        }

        public int GetNumberOfEntries(IEnumerable<BirthdayEntry> birthdayEntries)
        {
            return birthdayEntries.Count();
        }

        public void AddEntry(BirthdayEntry entry)
        {
            try
            {
                birthdayDataAccess.AddEntry(entry);
            }
            catch (Exception ex)
            {
                HandleBusinessError("Ошибка в бизнес-логике при добавлении записи", ex);
            }
        }

        public void RemoveEntry(int entryId)
        {
            try
            {
                birthdayDataAccess.RemoveEntry(entryId);
                UpdateEntryIds();
                birthdayDataAccess.SaveChangesInDb();
            }
            catch (Exception ex)
            {
                HandleBusinessError("Ошибка в бизнес-логике при удалении записи", ex);
            }
        }

        private void UpdateEntryIds()
        {
            var allEntries = birthdayDataAccess.GetAllBirthdays().OrderBy(x => x.EntryId).ToList();
            for (int i = 0; i < allEntries.Count; i++)
            {
                allEntries[i].EntryId = i + 1;
            }
        }

        public void UpdateEntry(BirthdayEntry entry)
        {
            try
            {
                birthdayDataAccess.UpdateEntry(entry);
            }
            catch (Exception ex)
            {
                HandleBusinessError("Ошибка в бизнес-логике при обновлении записи", ex);
            }
        }

        public IEnumerable<BirthdayEntry> SortBirthdaysByName()
        {
            return birthdayDataAccess.GetAllBirthdays().OrderBy(x => x.Name);        
        }

        public IEnumerable<BirthdayEntry> SortBirthdaysByDate()
        {
            return birthdayDataAccess.GetAllBirthdays().OrderBy(x => x.DateOfBirth);
        }

        private void HandleBusinessError(string errorMessage, Exception ex)
        {
            Console.WriteLine($"{errorMessage}: {ex.Message}");
            throw ex;
        }
    }
}
