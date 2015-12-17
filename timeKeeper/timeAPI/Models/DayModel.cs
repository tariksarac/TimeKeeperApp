using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace timeAPI.Models
{
    public class DayModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string Time { get; set; }
        public string Note { get; set; }
        public string Person { get; set; }
        public int PersonId { get; set; }
    }
}