using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using timeAPI.Models;
using timeBase;

namespace timeAPI.Controllers
{
    public class DetailsController : BaseApiController<Detail>
    {
        public DetailsController(baseInterface<Detail> depo) : base(depo) { }

        public Object GetAll(int page = 0)
        {
            int pageSize = 10;
            IEnumerable<Detail> query = timeDepo.Get().OrderByDescending(x => x.Day.Date);
            int totalPages = (int)Math.Ceiling((double)query.Count() / pageSize);
            IList<DetailModel> details = query.Skip(pageSize * page).Take(pageSize).ToList().Select(x => timeFact.Create(x)).ToList();
            return new
            {
                pageSize = pageSize,
                currentPage = page,
                totalPages = totalPages,
                details = details
            };
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

        public HttpResponseMessage Post(DetailModel detail)
        {
            try
            {
                using (timeContext ctx = new timeContext())
                {
                    Detail entity = timeFact.Parse(detail);
                    ctx.Details.Attach(entity);
                    ctx.Entry(entity).State = EntityState.Added;
                    ctx.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created, detail);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, DetailModel detail)
        {
            try
            {
                Detail newDetail = timeFact.Parse(detail);
                using (timeContext ctx = new timeContext())
                {
                    Detail oldDetail = ctx.Details.Find(id);
                    ctx.Entry(oldDetail).CurrentValues.SetValues(newDetail);
                    ctx.SaveChanges();
                    if (oldDetail.Day.Id != newDetail.Day.Id)
                        ctx.Database.ExecuteSqlCommand("update details set day_id=" + newDetail.Day.Id + " where id=" + newDetail.Id);
                    if (oldDetail.Project.Id != newDetail.Project.Id)
                        ctx.Database.ExecuteSqlCommand("update details set project_id=" + newDetail.Project.Id + " where id=" + newDetail.Id);
                    return Request.CreateResponse(HttpStatusCode.OK, detail);
                }
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
