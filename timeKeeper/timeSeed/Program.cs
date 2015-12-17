using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timeBase;

namespace timeSeed
{
    class Program
    {
        static string sourceData = @"E:\TimeKeeper\timeKeeper.xls";

        static void Main(string[] args)
        {
            getProjects();
            getRoles();
            getTeams();
            getPeople();
            getDays();
            getDetails();
            Console.ReadKey();
        }

        static void getProjects()
        {
            int N = 0;
            baseInterface<Project> projects = new baseRepository<Project>(new timeContext());
            Console.Write("Projects: ");
            DataTable rawData = OpenExcell(sourceData, "Projects");
            foreach (DataRow row in rawData.Rows)
            {
                Project project = new Project() { Name = row.ItemArray.GetValue(1).ToString() };
                projects.Insert(project);
                N++;
            }
            projects.Commit();
            Console.WriteLine(N);
        }

        static void getRoles()
        {
            int N = 0;
            baseInterface<Role> roles = new baseRepository<Role>(new timeContext());
            Console.Write("Roles: ");
            DataTable rawData = OpenExcell(sourceData, "Roles");
            foreach (DataRow row in rawData.Rows)
            {
                Role role = new Role() { Name = row.ItemArray.GetValue(1).ToString() };
                roles.Insert(role);
                N++;
            }
            roles.Commit();
            Console.WriteLine(N);
        }

        static void getTeams()
        {
            int N = 0;
            baseInterface<Team> teams = new baseRepository<Team>(new timeContext());
            Console.Write("Teams: ");
            DataTable rawData = OpenExcell(sourceData, "Teams");
            foreach (DataRow row in rawData.Rows)
            {
                Team team = new Team() { Name = row.ItemArray.GetValue(1).ToString() };
                teams.Insert(team);
                N++;
            }
            teams.Commit();
            Console.WriteLine(N);
        }

        static void getPeople()
        {
            int N = 0;
            timeContext ctx = new timeContext();
            baseInterface<Person> people = new baseRepository<Person>(ctx);
            baseInterface<Team> teams = new baseRepository<Team>(ctx);
            baseInterface<Role> roles = new baseRepository<Role>(ctx);
            Console.Write("People: ");
            DataTable rawData = OpenExcell(sourceData, "People");
            foreach (DataRow row in rawData.Rows)
            {
                Person person = new Person() { 
                    FirstName = row.ItemArray.GetValue(1).ToString(),
                    LastName = row.ItemArray.GetValue(2).ToString(),
                    Email = row.ItemArray.GetValue(5).ToString(),
                    Phone = row.ItemArray.GetValue(6).ToString(),
                    Team = teams.Get(Convert.ToInt32(row.ItemArray.GetValue(3).ToString())),
                    Role = roles.Get(Convert.ToInt32(row.ItemArray.GetValue(4).ToString()))
                };
                people.Insert(person);
                N++;
            }
            teams.Commit();
            Console.WriteLine(N);
        }

        static void getDays()
        {
            Console.Write("Days: ");
            DataTable rawData = OpenExcell(sourceData, "Days");
            int total = rawData.Rows.Count;
            int steps = (int)Math.Ceiling(total / 500.0);
            var rows = rawData.Rows;
            int N = 0;
            while (++N <= steps)
            {
                var days = new List<Day>();
                days.Clear();
                using (var ctx = new timeContext())
                {
                    baseInterface<Person> people = new baseRepository<Person>(ctx);
                    ctx.Configuration.AutoDetectChangesEnabled = false;
                    ctx.Configuration.ValidateOnSaveEnabled = false;
                    for (int j = 500 * (N - 1); j < 500 * N; j++)
                    {
                        if (j >= total)
                        {
                            ctx.SaveChanges();
                            break;
                        }
                        Day day = new Day()
                        {
                            Date = Convert.ToDateTime(rows[j].ItemArray.GetValue(1).ToString()),
                            Type = rows[j].ItemArray.GetValue(2).ToString(),
                            Time = Convert.ToDouble(rows[j].ItemArray.GetValue(3).ToString()),
                            Note = rows[j].ItemArray.GetValue(5).ToString(),
                            Person = people.Get(Convert.ToInt32(rows[j].ItemArray.GetValue(4).ToString()))
                        };
                        days.Add(day);
                    }
                    ctx.Days.AddRange(days);
                    ctx.SaveChanges();
                    Console.Write(N);
                }
            }
            Console.WriteLine(total);
        }

        static void getDetails()
        {
            Console.Write("Details: ");
            DataTable rawData = OpenExcell(sourceData, "Details");
            int total = rawData.Rows.Count;
            int steps = (int)Math.Ceiling(total / 500.0);
            var rows = rawData.Rows;
            int N = 0;
            while (++N <= steps)
            {
                var details = new List<Detail>();
                details.Clear();
                using (var ctx = new timeContext())
                {
                    baseInterface<Project> projects = new baseRepository<Project>(ctx);
                    baseInterface<Day> days = new baseRepository<Day>(ctx);
                    ctx.Configuration.AutoDetectChangesEnabled = false;
                    ctx.Configuration.ValidateOnSaveEnabled = false;
                    for (int j = 500 * (N - 1); j < 500 * N; j++)
                    {
                        if (j >= total)
                        {
                            ctx.SaveChanges();
                            break;
                        }
                        Detail detail = new Detail()
                        {
                            Time = Convert.ToDouble(rows[j].ItemArray.GetValue(3).ToString()),
                            Note = rows[j].ItemArray.GetValue(4).ToString(),
                            Status = "1",
                            Day = days.Get(Convert.ToInt32(rows[j].ItemArray.GetValue(1).ToString())),
                            Project = projects.Get(Convert.ToInt32(rows[j].ItemArray.GetValue(2).ToString()))
                        };
                        details.Add(detail);
                    }
                    ctx.Details.AddRange(details);
                    ctx.SaveChanges();
                    Console.Write(N);
                }
            }
            Console.WriteLine(total);
        }

        static DataTable OpenExcell(string path, string sheet)
        {   
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0");
            conn.Open();

            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "$]", conn);
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = cmd;

            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            conn.Close();

            return dt;
        }

    }
}
