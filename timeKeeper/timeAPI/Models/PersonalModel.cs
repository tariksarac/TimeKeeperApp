using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace timeAPI.Models
{
    public class ProjectDetail
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Project { get; set; }
        public double Time { get; set; }
        public string Note { get; set; }
    }

    public class DiaryModel
    {
        public int DayId { get; set; }
        public int Day { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
        public double Time { get; set; }
        public IList<ProjectDetail> Details { get; set; }
    }

    public class PersonalModel
    {
        private int DaysInMonth { get; set; }
        private DiaryModel[] _diary;

        public PersonalModel(int year, int month)
        {
            Year = year;
            Month = month;
            DaysInMonth = DateTime.DaysInMonth(Year, Month);
            Diary = new DiaryModel[DaysInMonth];
        }

        public DiaryModel[] Diary
        {
            get { return _diary; }
            set
            {
                _diary = new DiaryModel[DaysInMonth];
                for (int i = 0; i < DaysInMonth; i++)
                {
                    _diary[i] = new DiaryModel() { Day = i + 1, Time = 0, Note = "" };
                    DayOfWeek weekDay = new DateTime(Year, Month, i + 1).DayOfWeek;
                    _diary[i].Type = (weekDay == DayOfWeek.Saturday || weekDay == DayOfWeek.Sunday) ? "weekend" : "empty";
                }
            }
        }

        public int PersonId { get; set; }
        public string Person { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double TotalHours { get; set; }
        public int PtoDays { get; set; }
    }
}