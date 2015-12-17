using System.Collections.Generic;

namespace timeBase
{
    public class Team
    {
        public Team()
        {
            this.Persons = new List<Person>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
    }
}
