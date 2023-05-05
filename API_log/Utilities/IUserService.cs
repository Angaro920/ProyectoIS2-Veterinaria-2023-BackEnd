using API_Log.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Log.Utilities
{
    public interface IUserService
    {
        bool IsValid(LoginRequestModel req);
    }
}
