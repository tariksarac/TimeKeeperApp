using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using timeAPI.Filters;
using timeAPI.Models;
using timeBase;

namespace timeAPI.Controllers
{
    //[timeAuthorize]
    public class PersonsController : BaseApiController<Person>
    {
        public PersonsController(baseInterface<Person> depo) : base(depo) { }

        public Object GetAll(int page = 0)
        {
            int pageSize = 10;
            IEnumerable<Person> query = timeDepo.Get().OrderBy(x => x.LastName);
            int totalPages = (int)Math.Ceiling((double)query.Count() / pageSize);
            IList<PersonModel> people = query.Skip(pageSize * page).Take(pageSize).ToList().Select(x => timeFact.Create(x)).ToList();
            return new
            {
                pageSize = pageSize,
                currentPage = page,
                totalPages = totalPages,
                people = people
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

        public HttpResponseMessage Post(PersonModel person)
        {
            try
            {
                using (timeContext ctx = new timeContext())
                {
                    Person entity = timeFact.Parse(person);
                    ctx.Persons.Attach(entity);
                    ctx.Entry(entity.Team).State = EntityState.Unchanged;
                    ctx.Entry(entity).State = EntityState.Added;
                    ctx.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created, person);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, PersonModel person)
        {
            try
            {
                Person newPerson = timeFact.Parse(person);
                using (timeContext ctx = new timeContext())
                {
                    Person oldPerson = ctx.Persons.Find(id);
                    ctx.Entry(oldPerson).CurrentValues.SetValues(newPerson);
                    ctx.SaveChanges();
                    if (oldPerson.Team.Id != newPerson.Team.Id)
                        ctx.Database.ExecuteSqlCommand("update people set team_id=" + newPerson.Team.Id + " where id=" + newPerson.Id);
                    if (oldPerson.Role.Id != newPerson.Role.Id)
                        ctx.Database.ExecuteSqlCommand("update people set role_id=" + newPerson.Role.Id + " where id=" + newPerson.Id);
                    return Request.CreateResponse(HttpStatusCode.OK, person);
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
