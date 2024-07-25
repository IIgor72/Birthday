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
            this.birthdayManager = birthdayManager ?? throw new ArgumentNullException(nameof(birthdayManager));
        }

        public void ShowMenu()
        {
            Console.WriteLine("1. Показать список Дней Рождений");
            Console.WriteLine("2. Показать сегодняшние и ближайшие Дни Рождения");
            Console.WriteLine("3. Показать прошедшие Дни Рождения");
            Console.WriteLine("4. Добавить запись Дня Рождения");
            Console.WriteLine("5. Удалить запись Дня Рождения");
            Console.WriteLine("6. Редактировать запись Дня Рождения");
            Console.WriteLine("7. Сортировать список");
            Console.WriteLine("0. Выйти");
            Console.WriteLine();
        }

        public string GetUserInput(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                string userInput = Console.ReadLine();
                Console.WriteLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    throw new ArgumentException("Ошибка: Ввод не может быть пустым.");
                }
                else
                {
                    return userInput;
                }
            }
        }

        public void DisplayBirthdays(IEnumerable<BirthdayEntry> birthdays)
        {
            if (birthdays == null || !birthdays.Any())
            {
                throw new InvalidOperationException("Список пуст");
            }

            int maxIdLength = birthdays.Max(entry => entry.EntryId.ToString().Length);
            int maxNameLength = birthdays.Max(entry => entry.Name.Length);
            int maxDateLength = birthdays.Max(entry => entry.DateOfBirth.ToString("dd/MM/yyyy").Length);

            string separator = new string('-', maxIdLength + maxNameLength + maxDateLength + 10);
            /*число 10 для нормального вывода таблицы из-за разделителей между колонками*/

            foreach (var entry in birthdays)
            {
                Console.WriteLine(separator);
                Console.WriteLine($"| {entry.EntryId.ToString().PadRight(maxIdLength)} |" +
                    $" {entry.Name.PadRight(maxNameLength)} |" +
                    $" {entry.DateOfBirth.ToString("dd/MM/yyyy").PadRight(maxDateLength)} |");
            }
            Console.WriteLine(separator);
            Console.WriteLine();
        }

        public IEnumerable<BirthdayEntry> ChoosingTheOutputMethod(Enum _outputID)
        {
            IEnumerable<BirthdayEntry> birthdays = null;

            try
            {
                switch ((outputID)Convert.ToInt32(_outputID))
                {
                    case outputID.All:
                        birthdays = birthdayManager.GetAllBirthdays();
                        Console.WriteLine("Список всех дней рождения");
                        break;

                    case outputID.Upcoming:
                        int countDays = Convert.ToInt32(GetUserInput("Введите количество дней для просмотра ближайщих дней рождения"));
                        birthdays = birthdayManager.GetUpcomingBirthdays(countDays);
                        Console.WriteLine($"Список ближайших дней рождения (в течении {countDays} дней)");
                        break;

                    case outputID.Past:
                        birthdays = birthdayManager.GetPastBirthdays();
                        Console.WriteLine("Список дней рождения за прошедший месяц");
                        break;

                    default:
                        throw new ArgumentException("Ошибка: передан неверный вариант");
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Ошибка форматирования: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }

            return birthdays;
        }

        public void AddBirthdayEntry()
        {
            try
            {
                int Id = birthdayManager.GetNumberOfEntries(birthdayManager.GetAllBirthdays());
                string nameToAdd = GetUserInput("Введите имя");
                DateTime dateOfBirthToAdd = Convert.ToDateTime(GetUserInput("Введите дату рождения (в формате дд.мм.гггг)"));

                birthdayManager.AddEntry(new BirthdayEntry(Id + 1, nameToAdd, dateOfBirthToAdd));
                Console.WriteLine("Запись добавлена");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Ошибка форматирования: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
            Console.WriteLine();
        }

        public void RemoveBirthdayEntry()
        {
            try
            {
                int IdEntryToRemove = Convert.ToInt32(GetUserInput("Введите Id записи для удаления"));
                birthdayManager.RemoveEntry(IdEntryToRemove);
                Console.WriteLine("Запись удалена");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Ошибка форматирования: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
            Console.WriteLine();
        }

        public void UpdateBirthdayEntry()
        {
            try
            {
                int Id = Convert.ToInt32(GetUserInput("Введите Id записи для изменения"));
                string updatedName = GetUserInput("Введите новое имя");
                DateTime updatedDateOfBirth = Convert.ToDateTime(GetUserInput("Введите новую дату рождения (в формате дд.мм.гггг)"));
                birthdayManager.UpdateEntry(new BirthdayEntry(Id, updatedName, updatedDateOfBirth));
                Console.WriteLine("Запись обновлена");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Ошибка форматирования: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
            Console.WriteLine();
        }

        public void SortBirthdayList()
        {
            try
            {
                IEnumerable<BirthdayEntry> filtredList = new List<BirthdayEntry>();
                Console.WriteLine("1. По имени");
                Console.WriteLine("2. По дате рождения");

                int userInput = Convert.ToInt32(GetUserInput("Выберите по какому полю сортировать"));
                switch ((selectSort)userInput)
                {
                    case selectSort.ByName:
                        filtredList = birthdayManager.SortBirthdaysByName();
                        break;
                    case selectSort.ByDate:
                        filtredList = birthdayManager.SortBirthdaysByDate();
                        break;
                    default:
                        Console.WriteLine("Выбран неверный вариант");
                        return;
                }

                DisplayBirthdays(filtredList);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Введите число, а не символ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
            Console.WriteLine();
        }
    }
}
