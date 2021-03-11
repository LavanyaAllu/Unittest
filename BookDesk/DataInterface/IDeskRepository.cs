using System;
using System.Collections.Generic;
using System.Text;
using BookDesk.Domain;

namespace BookDesk.DataInterface
{
    public interface IDeskRepository
    {
        IEnumerable<Desk> GetAvailableDesks(DateTime data);
    }
}
