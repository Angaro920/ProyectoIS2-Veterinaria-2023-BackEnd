using API_Log.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Log.Utilities
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated(LoginRequestModel request, out string token);
    }
}
