using Birthday.Business_Logic;
using Birthday.Data_Access;
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
        private readonly BirthdayManager birthdayManager;
        public ConsoleUI(BirthdayManager birthdayManager)
        {
            this.birthdayManager = birthdayManager;
        }
        public void ShowMenu()
        {
            Console.WriteLine("1. Показать список Дней Рождений");
            Console.WriteLine("2. Показать сегодняшние и ближайшие Дни Рождений");
            Console.WriteLine("3. Добавить запись Дня Рождения");
            Console.WriteLine("4. Удалить запись Дня Рождения");
            Console.WriteLine("5. Редактировать запись Дня Рождения");
            Console.WriteLine("0. Выйти");
            Console.WriteLine();
        }

        public string GetUserInput(string promt)
        {
            Console.Write($"{promt}: ");
            return Console.ReadLine();
        }
        public void DisplayBirthdays(Enum _outputID)
        {
            IEnumerable<BirthdayEntry> birthdays;
            if (Convert.ToInt32(_outputID) == Convert.ToInt32(outputID.All) )
            {
                birthdays = birthdayManager.GetAllBirthdays();
            }
            else
            {
                int countDays = Convert.ToInt32(GetUserInput("Введите количество дней для просмотра ближайщих дней рождения"));
                birthdays = birthdayManager.GetUpcomingBirthdays(countDays);
            }
            Console.WriteLine();
            foreach (var entry in birthdays)
            {
                Console.WriteLine(entry.ToString());
            }
            Console.WriteLine();
        }

    }
}
