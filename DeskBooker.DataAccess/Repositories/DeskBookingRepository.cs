using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookDesk.DataInterface;
using BookDesk.Domain;

namespace DeskBooker.DataAccess.Repositories
{
    public class DeskBookingRepository : IDeskBookingRepository
    {
        private readonly DeskBookerContext _context;
        public DeskBookingRepository(DeskBookerContext context)
        {
            _context = context;
        }

        public IEnumerable<DeskBooking> GetAll()
        {
            return _context.DeskBooking.OrderBy(x => x.Date).ToList();
        }

        public void Save(DeskBooking desk)
        {
            _context.DeskBooking.Add(desk);
            _context.SaveChanges();
        }
    }
}
