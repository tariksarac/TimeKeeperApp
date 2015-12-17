using System.Collections.Generic;

namespace timeBase
{
    public class Person
    {
        public Person()
        {
            this.Days = new List<Day>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual Team Team { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Day> Days { get; set; }
    }
}
