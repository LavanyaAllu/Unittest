using System;
using System.Collections.Generic;
using System.Text;
using BookDesk.DataInterface;
using BookDesk.Domain;

namespace DeskBooker.DataAccess.Repositories
{
    public class DeskRepository : IDeskRepository
    {
        public IEnumerable<Desk> GetAvailableDesks(DateTime data)
        {
            throw new NotImplementedException();
        }
    }
}
