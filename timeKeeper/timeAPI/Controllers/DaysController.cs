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
    public class DaysController : BaseApiController<Day>
    {
        public DaysController(baseInterface<Day> depo) : base(depo) { }

        public Object GetAll(int page = 0)
        {
            int pageSize = 10;
            IEnumerable<Day> query = timeDepo.Get().OrderByDescending(x => x.Date);
            int totalPages = (int)Math.Ceiling((double)query.Count() / pageSize);
            IList<DayModel> days = query.Skip(pageSize * page).Take(pageSize).ToList().Select(x => timeFact.Create(x)).ToList();
            return new
            {
                pageSize = pageSize,
                currentPage = page,
                totalPages = totalPages,
                days = days
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

        public HttpResponseMessage Post(DayModel day)
        {
            try
            {
                using (timeContext ctx = new timeContext())
                {
                    Day entity = timeFact.Parse(day);
                    ctx.Days.Attach(entity);
                    ctx.Entry(entity).State = EntityState.Added;
                    ctx.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created, day);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, DayModel day)
        {
            try
            {
                Day newDay = timeFact.Parse(day);
                using (timeContext ctx = new timeContext())
                {
                    Day oldDay = ctx.Days.Find(id);
                    ctx.Entry(oldDay).CurrentValues.SetValues(newDay);
                    ctx.SaveChanges();
                    if (oldDay.Person.Id != newDay.Person.Id)
                        ctx.Database.ExecuteSqlCommand("update days set person_id=" + newDay.Person.Id + " where id=" + newDay.Id);
                    return Request.CreateResponse(HttpStatusCode.OK, day);
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
