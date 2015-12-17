using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace timeAPI.Models
{
    public class TokenRequestModel
    {
        public string ApiKey { get; set; }
        public string Signature { get; set; }
    }

    //{
    //    "apikey"="dGltZUtlZXBlcg==",
    //    "signature"="u1KIDPb3swI0EiEFWJosez+noK7FDoRpa2+2lyEAvfM="
    //}
}