using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace timeAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public int TeamId { get; set; }
        public string Team { get; set; }
    }
}