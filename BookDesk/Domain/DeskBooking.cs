using System.Collections.Generic;
using System.Text;

namespace BookDesk.Domain
{
    public class DeskBooking : DeskBookingBase
    {
        public int Id { get; set; }
        public int DeskId { get; set; }
    }
}
