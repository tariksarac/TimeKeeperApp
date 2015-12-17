using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace timeAPI.Models
{
    public class ProjectMonthModel
    {
        public string Project { get; set; }
        public double Hours { get; set; }
    }

    public class PersonMonthModel
    {
        public PersonMonthModel(string personName)
        {
            Person = personName;
            TotalHours = 0;
            PtoDays = 0;
            Projects = new List<ProjectMonthModel>();
        }

        public string Person { get; set; }
        public double TotalHours { get; set; }
        public int PtoDays { get; set; }
        public IList<ProjectMonthModel> Projects { get; set; }
    }

    public class MonthModel
    {
        public MonthModel(int year, int month)
        {
            Year = year;
            Month = month;
            People = new List<PersonMonthModel>();
        }

        public int Year { get; set; }
        public int Month { get; set; }
        public IList<PersonMonthModel> People { get; set; }
    }
}