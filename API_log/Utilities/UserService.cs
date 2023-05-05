using API_Log.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Log.Utilities
{
    public class UserService : IUserService
    {
        // Prueba de simulación, el valor predeterminado es verificación artificial efectiva
        public bool IsValid(LoginRequestModel req)
        {
            return true;
        }
    }
}
