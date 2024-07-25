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
        public void AddEntry(BirthdayEntry entry)
        {
            try
            {
                dbContext.BirthdayEntries.Add(entry);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении записи в базу данных: {ex.Message}");
                throw;
            }
        }
        public void RemoveEntry(int entryId)
        {
            try
            {
                var entryToRemove = dbContext.BirthdayEntries.Where(x => x.EntryId == entryId).FirstOrDefault();
                if (entryToRemove != null)
                {
                    dbContext.BirthdayEntries.Remove(entryToRemove);
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Запись не найдена");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении записи из базы данных: {ex.Message}");
                throw;
            }
        }

        public void UpdateEntry(BirthdayEntry updateEntry)
        {
            try
            {
                var entryToUpdate = dbContext.BirthdayEntries.Where(x => x.EntryId == updateEntry.EntryId).FirstOrDefault();
                if (entryToUpdate != null)
                {
                    entryToUpdate.Name = updateEntry.Name;
                    entryToUpdate.DateOfBirth = updateEntry.DateOfBirth;
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Запись для обновления не найдена");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении записи в базе данных: {ex.Message}");
            }
        }

        public void SaveChangesInDb()
        {
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении изменений в базу данных: {ex.Message}");
                throw;
            }
        }
    }
}
