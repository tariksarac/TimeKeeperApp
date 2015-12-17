using System;
using System.Collections.Generic;

namespace timeBase
{
    public class Day
    {
        public Day()
        {
            this.Details = new List<Detail>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public double Time { get; set; }
        public string Note { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<Detail> Details { get; set; }
    }
}
