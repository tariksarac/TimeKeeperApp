using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;
using timeAPI.Filters;
using timeAPI.Services;
using WebMatrix.WebData;

namespace timeAPI.Controllers
{
    [timeAuthorize]
    public class LoginController : ApiController
    {
        ItimeIdentity ident = new timeIdentity();

        public HttpResponseMessage Get(int id = 0)
        {
            if (id == 1)
            {
                if (!WebSecurity.Initialized) WebSecurity.InitializeDatabaseConnection
                    ("timeContext", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                WebSecurity.Logout();
                Thread.CurrentPrincipal = null;
                HttpContext.Current.User = null;
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                if (ident.currentUser == null)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
                else
                    return Request.CreateResponse(HttpStatusCode.OK, ident.currentUser);
            }
        }
    }

}
