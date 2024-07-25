using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Birthday.Business_Logic;
using Birthday.Data_Access;
using Birthday.Model;

namespace Birthday.UI
{
    enum outputID { All, Upcoming, Past }
    enum selectSort { ByName = 1, ByDate }
    internal class Program
    {
        static void Main(string[] args)
        {
            BirthdayDbContext db = new BirthdayDbContext();
            IBirthdayDataAccess birthdayDataAccess = new BirthdayDataAccess(db);
            BirthdayManager birthdayManager = new BirthdayManager(birthdayDataAccess);
            ConsoleUI ui = new ConsoleUI(birthdayManager);

            while (true)
            {
                try
                {
                    ui.ShowMenu();
                    switch (Convert.ToInt32(ui.GetUserInput("Выберите пункт меню")))
                    {
                        case 1:
                            ui.DisplayBirthdays(ui.ChoosingTheOutputMethod(outputID.All));
                            break;

                        case 2:
                            ui.DisplayBirthdays(ui.ChoosingTheOutputMethod(outputID.Upcoming));
                            break;

                        case 3:
                            ui.DisplayBirthdays(ui.ChoosingTheOutputMethod(outputID.Past));
                            break;

                        case 4:
                            ui.AddBirthdayEntry();
                            break;

                        case 5:
                            ui.RemoveBirthdayEntry();
                            break;

                        case 6:
                            ui.UpdateBirthdayEntry();
                            break;

                        case 7:
                            ui.SortBirthdayList();
                            break;

                        case 0:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Выбран не существующий пункт. Попробуйте еще раз.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: Введено некорректное значение. Пожалуйста, введите число.");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Произошла ошибка ввода-вывода: {ex.Message}. Попробуйте еще раз.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message} Попробуйте еще раз.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}. Попробуйте еще раз.");
                }
            }
        }
    }
}
