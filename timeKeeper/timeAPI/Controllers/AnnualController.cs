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
    public class AnnualController : ApiController
    {
        private IReports reports;

        public AnnualController(IReports _reports)
        {
            reports = _reports;
        }

        public AnnualModel Get(int year = 0)
        {
            return reports.GetAnnualReport(year);
        }
    }
}
