using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using timeAPI.Models;
using timeBase;

namespace timeAPI.Controllers
{
    public class BaseApiController<T> : ApiController
    {
        private baseInterface<T> depo;
        private ModelFactory fact;

        public BaseApiController(baseInterface<T> _depo)
        {
            depo = _depo;
        }

        protected baseInterface<T> timeDepo
        {
            get { return depo; }
        }

        protected ModelFactory timeFact
        {
            get
            {
                if (fact == null) fact = new ModelFactory();
                return fact;
            }
        }
    }
}
