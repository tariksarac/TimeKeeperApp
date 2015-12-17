using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace timeAPI.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public List<PersonModel> Members { get; set; }
    }
}