using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using timeAPI.BusinessLogic;
using timeAPI.Models;

namespace timeAPI.Controllers
{
    public class MonthController : ApiController
    {
        private IReports reports;

        public MonthController(IReports _reports)
        {
            reports = _reports;
        }

        public MonthModel Get(int year = 0, int month = 0)
        {
            return reports.GetMonthReport(year, month);
        }
    }
}
