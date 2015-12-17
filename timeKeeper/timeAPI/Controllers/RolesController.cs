using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using timeAPI.Models;
using timeBase;

namespace timeAPI.Controllers
{
    public class RolesController : BaseApiController<Role>
    {
        public RolesController(baseInterface<Role> depo) : base(depo) { }

        public List<RoleModel> Get()
        {
            return timeDepo.Get().ToList().Select(x => timeFact.Create(x, false)).ToList();
        }

        public HttpResponseMessage Get(int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, timeFact.Create(timeDepo.Get(id), true));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Post(RoleModel role)
        {
            try
            {
                timeDepo.Insert(timeFact.Parse(role));
                timeDepo.Commit();
                return Request.CreateResponse(HttpStatusCode.Created, role);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, RoleModel role)
        {
            try
            {
                var newRole = timeFact.Parse(role);
                var oldRole = timeDepo.Get(id);
                timeDepo.Update(oldRole, newRole);
                timeDepo.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, role);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                timeDepo.Delete(timeDepo.Get(id));
                timeDepo.Commit();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}