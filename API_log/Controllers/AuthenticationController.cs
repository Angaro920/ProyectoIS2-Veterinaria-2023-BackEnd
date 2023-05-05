using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using API_Log.RequestModels;
using API_Log.ResponseModels;
using API_Log.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Log.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        public AuthenticationController(IAuthenticationService authService)
        {
            this._authService = authService;
        }
        [AllowAnonymous]
        [HttpPost, Route("requestToken")]
        public ActionResult RequestToken([FromBody] LoginRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }

            LoginResponseModel loginResponseModel = new LoginResponseModel();
            loginResponseModel.Respuesta = 0;
            loginResponseModel.Token = null;
            loginResponseModel.Mensaje = "Usuario y/o Contraseña Incorrectos";

            string token;
            if (_authService.IsAuthenticated(request, out token))
            {
                loginResponseModel.Respuesta = 1;
                loginResponseModel.Token = token;
                loginResponseModel.Mensaje = "Login Exitoso";
            }

            return Ok(loginResponseModel);

        }
    }
}
