using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using timeAPI.BusinessLogic;
using timeAPI.Filters;

namespace timeAPI.Controllers
{
    //[timeAuthorize(true)]
    public class PersonalController : ApiController
    {
        private IReports reports;

        public PersonalController(IReports _reports)
        {
            reports = _reports;
        }

        public HttpResponseMessage Get(int id = 0, int year = 0, int month = 0)
        {
            var personalReport = reports.GetPersonalReport(id, year, month);
            if (personalReport == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, personalReport);
        }
    }
}
