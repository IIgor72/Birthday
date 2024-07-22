using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Birthday.Model;

namespace Birthday.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            using (BirthdayDbContext db = new BirthdayDbContext())
            {
                /*                // создаем два объекта User

                                BirthdayEntry user1 = new BirthdayEntry { Name = "шопшп",  DateOfBirth = new DateTime(2020, 12, 12) };
                                BirthdayEntry user2 = new BirthdayEntry { Name = "Alice", DateOfBirth = new DateTime(2020, 11, 06) };

                                // добавляем их в бд
                                db.BirthdayEntries.Add(user1);
                                db.BirthdayEntries.Add(user2);
                                db.SaveChanges();
                                Console.WriteLine("Объекты успешно сохранены");*/

                // получаем объекты из бд и выводим на консоль
                var users = db.BirthdayEntries.ToList();
                Console.WriteLine("Список объектов:");
                foreach (BirthdayEntry t in users)
                {
                    Console.WriteLine($"{t.Id}.{t.Name} - {t.DateOfBirth.ToString("dd/MM/yyyy")}");
                }
            }
            Console.Read();
        }
    }
}
