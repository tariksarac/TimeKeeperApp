using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using timeAPI.Models;
using timeAPI.Services;
using timeBase;

namespace timeAPI.BusinessLogic
{
    public class Reports : IReports
    {
        timeContext ctx = new timeContext();
        ItimeIdentity ident = new timeIdentity();

        public AnnualModel GetAnnualReport(int year)
        {
            if (year == 0) year = Convert.ToInt32(ConfigurationManager.AppSettings["currentYear"]);

            AnnualModel result = new AnnualModel()  { Year = year, ItemList = new List<AnnualItem>() };
            AnnualItem total = new AnnualItem() { ProjectName = "TOTAL" };

            using (ctx)
            {
                var projects = ctx.Projects.OrderBy(x => x.Name).ToList();
                foreach (var project in projects)
                {
                    AnnualItem projectItem = new AnnualItem() { ProjectName = project.Name };
                    var details = project.Details.Where(x => x.Day.Date.Year == year)
                                                 .GroupBy(x => x.Day.Date.Month)
                                                 .Select(x => new { month = x.Key, hours = x.Sum(d => d.Time) });
                    foreach (var detail in details)
                    {
                        total.TotalHours += detail.hours;
                        total.MonthlyHours[detail.month - 1] += detail.hours;
                        projectItem.TotalHours += detail.hours;
                        projectItem.MonthlyHours[detail.month - 1] = detail.hours;
                    }
                    if (projectItem.TotalHours > 0) result.ItemList.Add(projectItem);
                }
                result.ItemList.Add(total);
            }
            return result;
        }

        public PersonalModel GetPersonalReport(int id, int year, int month)
        {
            if (year == 0) year = Convert.ToInt32(ConfigurationManager.AppSettings["currentYear"]);
            if (month == 0) month = DateTime.Today.Month;
            if (id == 0) id = ident.currentUser.Id;

            PersonalModel result = new PersonalModel(year, month);

            using (ctx)
            {
                Person person = ctx.Persons.Find(id);

                if (person == null) return null;
                
                result.Person = person.FirstName + " " + person.LastName;

                var days = person.Days.Where(x => x.Date.Year == year && x.Date.Month == month).OrderBy(x => x.Date.Day).ToList();
                foreach (var day in days)
                {
                    DiaryModel diary = new DiaryModel() { DayId = day.Id, Day = day.Date.Day, Note = day.Note, Time = day.Time };
                    result.TotalHours += day.Time;
                    if (day.Type == "2")
                    {
                        diary.Type = "vacation";
                        result.PtoDays++;
                    }
                    else
                        diary.Type = "workday";

                    diary.Details = new List<ProjectDetail>();
                    var details = day.Details.ToList();
                    foreach (var detail in details)
                    {
                        ProjectDetail proDet = new ProjectDetail();
                        proDet.Id = detail.Id;
                        proDet.ProjectId = detail.Project.Id;
                        proDet.Project = detail.Project.Name;
                        proDet.Note = detail.Note;
                        proDet.Time = detail.Time;
                        diary.Details.Add(proDet);
                    }
                    result.Diary[diary.Day - 1] = diary;
                }
            }
            return result;
        }

        public MonthModel GetMonthReport(int year, int month)
        {
            if (year == 0) year = Convert.ToInt32(ConfigurationManager.AppSettings["currentYear"]);
            if (month == 0) month = DateTime.Today.Month;

            MonthModel result = new MonthModel(year, month);

            using (ctx)
            {
                IList<ProjectMonthModel> projectList = new List<ProjectMonthModel>();
                var details = ctx.Details.Where(d => d.Day.Date.Year == year && d.Day.Date.Month == month)
                                          .GroupBy(p => p.Project.Name)
                                          .Select(x => new { project = x.Key, hours = x.Sum(t => t.Time) }).ToList();
                double total = 0;
                foreach (var detail in details)
                    if (detail.hours > 0)
                    {
                        projectList.Add(new ProjectMonthModel { Project = detail.project, Hours = detail.hours });
                        total += detail.hours;
                    }
                result.People.Add(new PersonMonthModel("T O T A L")
                {
                    Projects = projectList,
                    TotalHours = total,
                    PtoDays = ctx.Days.Where(d => d.Type == "2" && d.Date.Month == month && d.Date.Year == year).Count()
                });

                var people = ctx.Persons.OrderBy(x => x.LastName).ToList();
                foreach (var person in people)
                {
                    var personItem = new PersonMonthModel(person.FirstName + " " + person.LastName);
                    personItem.PtoDays = person.Days.Where(d => d.Type == "2" && d.Date.Month == month && d.Date.Year == year).Count();
                    foreach (var project in projectList)
                        personItem.Projects.Add(new ProjectMonthModel { Project = project.Project, Hours = 0 });

                    var dets = person.Days.Where(x => x.Date.Month == month && x.Date.Year == year).SelectMany(d => d.Details)
                                          .GroupBy(d => d.Project.Name)
                                          .Select(x => new { project = x.Key, hours = x.Sum(d => d.Time) }).ToList();
                    foreach(var det in dets)
                    {
                        foreach (var pit in personItem.Projects)
                        {
                            if (pit.Project == det.project)
                            {
                                personItem.TotalHours += det.hours;
                                pit.Hours = det.hours;
                                break;
                            }
                        }
                    }
                    if (personItem.TotalHours > 0) result.People.Add(personItem);
                }
            }
            return result;
        }
    }
}