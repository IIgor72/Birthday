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
            return null;
        }
    }
}
