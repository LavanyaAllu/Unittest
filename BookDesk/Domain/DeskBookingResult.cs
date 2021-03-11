using System;
using System.Collections.Generic;
using System.Text;

namespace BookDesk.Domain
{
    public class DeskBookingResult : DeskBookingBase
    {
        public int? DeskBookingId { get; set; }

        public DeskBookingResultCode Code { get; set; }
    }
}
