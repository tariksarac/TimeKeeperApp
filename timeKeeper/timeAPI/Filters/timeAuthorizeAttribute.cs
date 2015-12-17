using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using timeBase;
using WebMatrix.WebData;

namespace timeAPI.Filters
{
    public class timeAuthorizeAttribute : AuthorizationFilterAttribute
    {
        private bool perUser;
        public timeAuthorizeAttribute(bool _perUser = true)
        {
            perUser = _perUser;
        }

        private timeContext ctx = new timeContext();

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //var query = HttpUtility.ParseQueryString(actionContext.Request.RequestUri.Query);
            //if (!string.IsNullOrWhiteSpace("apikey") && !string.IsNullOrWhiteSpace("token"))
            //{
            //    var apiKey = query["apikey"];
            //    var token = query["token"];

            //    AuthToken authToken = ctx.AuthTokens.Include("User").Where(t => t.Token == token).FirstOrDefault();

            //    if (authToken != null && authToken.User.AppId == apiKey && authToken.Expiration > DateTime.UtcNow)
            //    {
            //        if (perUser)
            //        {
                        if (Thread.CurrentPrincipal.Identity.IsAuthenticated) return;

                        var authHeader = actionContext.Request.Headers.Authorization;
                        if (authHeader != null)
                        {
                            if (authHeader.Scheme.ToLower() == "basic" && authHeader.Parameter != "")
                            {
                                var rawCredentials = authHeader.Parameter;
                                var encoding = Encoding.GetEncoding("iso-8859-1");
                                string credentials = encoding.GetString(Convert.FromBase64String(rawCredentials));
                                string[] split = credentials.Split(':');
                                string username = split[0];
                                string password = split[1];

                                if (!WebSecurity.Initialized) WebSecurity.InitializeDatabaseConnection("timeContext", "UserProfile", "UserId", "UserName", autoCreateTables: true);

                                if (WebSecurity.Login(username, password))
                                {
                                    var principal = new GenericIdentity(username);
                                    Thread.CurrentPrincipal = new GenericPrincipal(principal, null);
                                    if (HttpContext.Current != null)
                                    {
                                        HttpContext.Current.User = Thread.CurrentPrincipal;
                                    }
                                    return;
                                }
                            }
                        }
            //        }
            //    }
            //}
            HandleUnauthorized(actionContext);
        }

        void HandleUnauthorized(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='timeContext' location=''");
            // location='http://localhost:444/accounts/login'
        }

    }
}