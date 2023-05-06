using API_Log.Context;
using API_Log.Helper;
using API_Log.Models;
using API_Log.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleLogin.Helper;
using System;
using System.Linq;

namespace API_Log.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VariosController : Controller
    {
        private readonly AppDbContext context;

        public VariosController(AppDbContext context)
        {
            this.context = context;
        }
        
        // POST: VariosController/Create
        [HttpPost]
        [Route("Registrar")]
        public ActionResult Post([FromBody] TblEmpleados empleado)
        {
             try 
            { 
                var empleadoExist= context.TblEmpleados.Where(p=>p.IdEmpleado ==empleado.IdEmpleado).FirstOrDefault();
                if (empleadoExist!=null)
                {
                    return Ok("Username ya registrado");
                
                }
                else
                {
                    var hash = Encrypt.GetSHA256(empleado.Contraseña);
                    empleado.Contraseña = hash;

                    context.TblEmpleados.Add(empleado);
                    context.SaveChanges();

                }
                return Ok("Registrado");
            }

            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
