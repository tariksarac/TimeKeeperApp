using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using timeAPI.Models;
using timeBase;

namespace timeAPI.Controllers
{
    public class TokenController : ApiController
    {
        timeSign signature = new timeSign();
        timeContext ctx = new timeContext();
        ModelFactory fact = new ModelFactory();

        public HttpResponseMessage Post(TokenRequestModel tokenModel)
        {
            try
            {
                var user = ctx.ApiUsers.Where(x => x.AppId == tokenModel.ApiKey).FirstOrDefault();

                if (user != null)
                {      
                    byte[] secret = Convert.FromBase64String(user.Secret);
                    byte[] apikey = Convert.FromBase64String(user.AppId);

                    if (signature.getSignature(secret, apikey) == tokenModel.Signature)
                    {
                        Random rnd = new Random();
                        var strToken = rnd.Next(99999999).ToString("00000000");

                        AuthToken authToken = new AuthToken()
                        {
                            Token = strToken,
                            Expiration = DateTime.UtcNow.AddMinutes(20),
                            User = user
                        };
                        ctx.AuthTokens.Add(authToken);
                        ctx.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, fact.Create(authToken));
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
