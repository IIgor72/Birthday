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
    enum outputID { All, Upcoming }
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
                ui.ShowMenu();
                switch(Convert.ToInt32(ui.GetUserInput("Выберите пункт меню")))
                {
                    case 1:
                        ui.DisplayBirthdays(outputID.All);
                        break;
                
                    case 2:
                        ui.DisplayBirthdays(outputID.Upcoming);
                        break;

                    case 3:
                        string nameEntryToAdd = ui.GetUserInput("Введите имя");
                        DateTime dateOfBirthEntryToAdd = Convert.ToDateTime(ui.GetUserInput("Введите дату рождения (в формате 0000.00.00)"));
                        BirthdayEntry entryToAdd = new BirthdayEntry(nameEntryToAdd, dateOfBirthEntryToAdd);
                        birthdayManager.AddEntry(entryToAdd);
                        break;

                    case 4:
                        int IdEntryToRemove = Convert.ToInt32(ui.GetUserInput("Введите Id записи для удаления"));
                        birthdayManager.RemoveEntry(IdEntryToRemove);
                        break;

                    case 5:
                        string nameEntryToUpdate = ui.GetUserInput("Введите новое имя");
                        DateTime dateOfBirthEntryToUpdate = Convert.ToDateTime(ui.GetUserInput("Введите новую дату рождения (в формате 0000.00.00)"));
                        BirthdayEntry entryToUpdate = new BirthdayEntry(nameEntryToUpdate, dateOfBirthEntryToUpdate);
                        birthdayManager.AddEntry(entryToUpdate);
                        break;

                    case 6:
                        break;

                    case 7:
                        break;

                    case 0:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
