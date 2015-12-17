using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace timeAPI.Models
{
    public class DetailModel
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public int ProjectId { get; set; }
        public string Project { get; set; }
        public int DayId { get; set; }
        public string Day { get; set; }
    }
}