using System;
using System.Collections.Generic;
using System.Text;
using BookDesk.Domain;

namespace BookDesk.DataInterface
{
    public interface IDeskBookingRepository
    {
        void Save(DeskBooking desk);
        IEnumerable<DeskBooking> GetAll();
    }
}