using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace timeAPI.Models
{
    public class AnnualItem
    {
        public AnnualItem()
        {
            MonthlyHours = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            TotalHours = 0;
        }
        public string ProjectName { get; set; }
        public double[] MonthlyHours { get; set; }
        public double TotalHours { get; set; }
    }

    public class AnnualModel
    {
        public int Year { get; set; }
        public IList<AnnualItem> ItemList { get; set; }
    }
}