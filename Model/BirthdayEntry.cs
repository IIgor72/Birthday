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
        public int EntryId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public BirthdayEntry(int entryId, string name, DateTime dateOfBirth)
        {
            EntryId = entryId;
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public override string ToString()
        {
            return $"{EntryId}.{Name} - {DateOfBirth.ToString("dd/MM/yyyy")}";
        }
    }
}
