using System.Collections.Generic;

namespace timeBase
{
    public class Project
    {
        public Project()
        {
            this.Details = new List<Detail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Detail> Details { get; set; }
    }
}
