using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timeAPI.Models;

namespace timeAPI.BusinessLogic
{
    public interface IReports
    {
        PersonalModel GetPersonalReport(int id, int year = 0, int month = 0);
        MonthModel GetMonthReport(int year = 0, int month = 0);
        AnnualModel GetAnnualReport(int year = 0);
    }
}
