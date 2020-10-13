using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuttleOffServiceAjax
{
    public class ReservationDetails
    {
        public int schedId { get; set; }
        public int userId { get; set; }

        public int reserveId { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string DateEffective { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}