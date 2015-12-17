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
    public class ProjectsController : BaseApiController<Project>
    {
        public ProjectsController(baseInterface<Project> depo) : base(depo) { }

        public List<ProjectModel> Get()
        {
            return timeDepo.Get().ToList().Select(x => timeFact.Create(x)).ToList();
        }

        public HttpResponseMessage Get(int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, timeFact.Create(timeDepo.Get(id)));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Post(ProjectModel project)
        {
            try
            {
                timeDepo.Insert(timeFact.Parse(project));
                timeDepo.Commit();
                return Request.CreateResponse(HttpStatusCode.Created, project);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, ProjectModel project)
        {
            try
            {
                var newProject = timeFact.Parse(project);
                var oldProject = timeDepo.Get(id);
                timeDepo.Update(oldProject, newProject);
                timeDepo.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, project);
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
