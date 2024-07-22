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
           this.birthdayDataAccess = birthdayDataAccess;
        }
        public IEnumerable<BirthdayEntry> GetAllBirthdays()
        {
            return birthdayDataAccess.GetAllBirthdays();
        }
        public IEnumerable<BirthdayEntry> GetUpcomingBirthdays(int countDays)
        {
            DateTime currentDate = DateTime.Today;
            DateTime endDate = currentDate.AddDays(countDays);
            return birthdayDataAccess.GetAllBirthdays().Where(x => IsDateInRange(x.DateOfBirth, currentDate, endDate)).ToList();
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
        public void AddEntry(BirthdayEntry entry)
        {
            birthdayDataAccess.AddEntry(entry);
        }
        public void RemoveEntry(int entryId)
        {
            birthdayDataAccess.RemoveEntry(entryId);
        }
        public void UpdateEntry(BirthdayEntry entry)
        {
            birthdayDataAccess.UpdateEntry(entry);
        }
    }
}
