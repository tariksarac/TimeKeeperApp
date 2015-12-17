using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using timeBase;

namespace timeAPI.Models
{
    public class ModelFactory
    {
        public TeamModel Create(Team team, bool withMembers)
        {
            return new TeamModel
            {
                Id = team.Id,
                Name = team.Name,
                Size = team.Persons.Count(),
                Members = (withMembers) ? team.Persons.Select(x => Create(x)).ToList() : null
            };
        }

        public Team Parse(TeamModel team)
        {
            return new Team
            {
                Id = team.Id,
                Name = team.Name,
            };
        }

        public RoleModel Create(Role role, bool withMembers)
        {
            return new RoleModel
            {
                Id = role.Id,
                Name = role.Name,
                Size = role.Persons.Count(),
                Members = (withMembers) ? role.Persons.Select(x => Create(x)).ToList() : null
            };
        }

        public Role Parse(RoleModel role)
        {
            return new Role
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public ProjectModel Create(Project project)
        {
            return new ProjectModel
            {
                Id = project.Id,
                Name = project.Name
            };
        }

        public Project Parse(ProjectModel project)
        {
            return new Project
            {
                Id = project.Id,
                Name = project.Name
            };
        }

        public PersonModel Create(Person person)
        {
            return new PersonModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                Phone = person.Phone,
                Team = person.Team.Name,
                TeamId = person.Team.Id,
                Role = person.Role.Name,
                RoleId = person.Role.Id
            };
        }

        public Person Parse(PersonModel person)
        {
            using (timeContext ctx = new timeContext())
            {
                return new Person
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Email = person.Email,
                    Phone = person.Phone,
                    Team = ctx.Teams.Find(person.TeamId),
                    Role = ctx.Roles.Find(person.RoleId)
                };
            }
        }

        public DayModel Create(Day day)
        {
            return new DayModel
            {
                Id = day.Id,
                Date = day.Date.ToString("dd.MM.yyyy"),
                Type = day.Type,
                Time = day.Time.ToString("#0.0"),
                Note = day.Note,
                Person = day.Person.FirstName + " " + day.Person.LastName,
                PersonId = day.Person.Id
            };
        }

        public Day Parse(DayModel day)
        {
            baseInterface<Person> persons = new baseRepository<Person>(new timeContext());
            return new Day
            {
                Id = day.Id,
                Date = Convert.ToDateTime(day.Date),
                Type = day.Type,
                Time = Convert.ToDouble(day.Time),
                Note = day.Note,
                Person = persons.Get(day.PersonId)
            };
        }

        public DetailModel Create(Detail detail)
        {
            return new DetailModel
            {
                Id = detail.Id,
                Day = detail.Day.Date.ToString("dd.MM.yyyy"),
                DayId = detail.Day.Id,
                Note = detail.Note,
                Status = detail.Status,
                Time = detail.Time.ToString("#0.0"),
                Project = detail.Project.Name,
                ProjectId = detail.Project.Id
            };
        }

        public Detail Parse(DetailModel detail)
        {
            using (timeContext ctx = new timeContext())
            {
                baseInterface<Project> projects = new baseRepository<Project>(ctx);
                baseInterface<Day> days = new baseRepository<Day>(ctx);
                return new Detail
                {
                    Id = detail.Id,
                    Time = Convert.ToDouble(detail.Time),
                    Note = detail.Note,
                    Status = detail.Status,
                    Day = days.Get(detail.DayId),
                    Project = projects.Get(detail.ProjectId)
                };
            }
        }

        public TokenModel Create(AuthToken token)
        {
            return new TokenModel
            {
                Token = token.Token,
                Expiration = token.Expiration
            };
        }
    }
}