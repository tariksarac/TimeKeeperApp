using System.Data.Entity;
using timeBase.Mappers;

namespace timeBase
{
    public class timeContext : DbContext
    {
        public timeContext(): base("name=timeContext")
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<ApiUser> ApiUsers { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApiUserMap());
            modelBuilder.Configurations.Add(new AuthTokenMap());
            modelBuilder.Configurations.Add(new DayMap());
            modelBuilder.Configurations.Add(new DetailMap());
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new TeamMap());

            //modelBuilder.Entity<Person>().HasRequired(x => x.Team).WithMany(p => p.Persons).Map(m => m.MapKey("Team"));
            //modelBuilder.Entity<Person>().HasRequired(x => x.Role).WithMany(p => p.Persons).Map(m => m.MapKey("Role"));
            //modelBuilder.Entity<Day>().HasRequired(x => x.Person).WithMany(p => p.Days).Map(m => m.MapKey("Person"));
            //modelBuilder.Entity<Detail>().HasRequired(x => x.Day).WithMany(p => p.Details).Map(m => m.MapKey("Day"));
            //modelBuilder.Entity<Detail>().HasRequired(x => x.Project).WithMany(p => p.Details).Map(m => m.MapKey("Project"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
