using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace timeAPI.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}