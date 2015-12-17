using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timeAPI.Models;

namespace timeAPI.Services
{
    public interface ItimeIdentity
    {
        UserModel currentUser { get; }
    }
}
