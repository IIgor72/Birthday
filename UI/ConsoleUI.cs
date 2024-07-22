using Birthday.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.UI
{
    internal class ConsoleUI
    {
        public void ShowMenu()
        {
            Console.WriteLine("1. Показать список Дней Рождений");
            Console.WriteLine("2. Показать сегодняшние и ближайшие Дни Рождений");
            Console.WriteLine("3. Добавить запись Дня Рождения");
            Console.WriteLine("4. Удалить запись Дня Рождения");
            Console.WriteLine("5. Редактировать запись Дня Рождения");
            Console.WriteLine("6. Выйти");
        }

        public string GetUserInput(string promt)
        {
            Console.Write($"{promt}: ");
            return Console.ReadLine();
        }
        public void DisplayBirthdays(IEnumerable<BirthdayEntry> birthdays)
        {
            foreach (var entry in birthdays)
            {
                Console.WriteLine(entry);
            }
        }
    }
}
