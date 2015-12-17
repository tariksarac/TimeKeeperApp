using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using timeAPI.Models;
using timeBase;

namespace timeAPI.Services
{
    public class timeIdentity : ItimeIdentity
    {
        public UserModel currentUser
        {
            get
            {
                if (!Thread.CurrentPrincipal.Identity.IsAuthenticated) return null;
                string username = Thread.CurrentPrincipal.Identity.Name;

                if (username == null || username == "") username = HttpContext.Current.User.Identity.Name;

                var person = (new baseRepository<Person>(new timeContext())).Get().Where(x => x.FirstName == username).FirstOrDefault();
                if (person == null)
                    return null;
                else
                    return new UserModel
                    {
                        Id = person.Id,
                        UserName = person.FirstName,
                        FullName = person.FirstName + " " + person.LastName,
                        RoleId = person.Role.Id,
                        Role = person.Role.Name,
                        TeamId = person.Team.Id,
                        Team = person.Team.Name
                    };
            }
        }
    }
}