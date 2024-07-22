using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Birthday.Model
{
    internal class BirthdayEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public BirthdayEntry() { }
        public BirthdayEntry(string name, DateTime dateOfBirth)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public override string ToString()
        {
            return $"{Id}.{Name} - {DateOfBirth.ToString("dd/MM/yyyy")}";
        }
    }
}
