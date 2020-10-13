using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuttleOffServiceAjax
{
    public class CourtDetails
    {
        public int CourtId { get; set; }
        public int SchedId { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string DateEffective { get; set; }
    }
}