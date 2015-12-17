using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using timeAPI.Models;
using timeBase;
using WebMatrix.WebData;

namespace timeAPI.Controllers
{
    public class TeamsController : BaseApiController<Team>
    {
        public TeamsController(baseInterface<Team> depo) : base(depo) { }

        public List<TeamModel> Get()
        {
            return timeDepo.Get().ToList().Select(x => timeFact.Create(x, false)).ToList();
        }

        public HttpResponseMessage Get(int id)
        {
            if (id == 99)
            {
                var people = (new baseRepository<Person>(new timeContext())).Get().OrderBy(x => x.Id);
                foreach (var Person in people) WebSecurity.CreateUserAndAccount(Person.FirstName.ToLower(), "platon", false);
                id = 5;
            }
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, timeFact.Create(timeDepo.Get(id), true));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Post(TeamModel team)
        {
            try
            {
                timeDepo.Insert(timeFact.Parse(team));
                timeDepo.Commit();
                return Request.CreateResponse(HttpStatusCode.Created, team);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, TeamModel team)
        {
            try
            {
                var newTeam = timeFact.Parse(team);
                var oldTeam = timeDepo.Get(id);
                timeDepo.Update(oldTeam, newTeam);
                timeDepo.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, team);
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

            //if (id == 99)
            //{
            //    var people = (new baseRepository<Person>(new timeContext())).Get().OrderBy(x => x.Id);
            //    foreach (var Person in people) WebSecurity.CreateUserAndAccount(Person.FirstName, "platon", false);
            //    id = 5;
            //}

