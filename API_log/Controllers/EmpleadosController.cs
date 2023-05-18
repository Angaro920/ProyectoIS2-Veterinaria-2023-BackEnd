using API_Log.Context;
using API_Log.Models;
using API_Log.RequestModels;
using API_Log.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Threading.Tasks;

namespace API_Log.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : Controller
    {
        public readonly AppDbContext dbContext;
        public EmpleadoController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmpleados()
        {

            var resp = await dbContext.TblEmpleados.Select(m => new
            {
                m.IdEmpleado,
                m.Usuario,
                m.Contraseña,
                Estado = new
                {
                    m.FkIdEstadoNavigation.IdEstado,
                    m.FkIdEstadoNavigation.Nombre
                }
            }).ToListAsync();
            return Ok(resp);
        }
        /*   [HttpGet]
           [Route("{IdEmpleado}")]
           public async Task<IActionResult> GetEmpleado([FromRoute] int IdEmpleado)
           {
               var Empleado = await dbContext.TblEmpleados.Where(e => e.IdEmpleado == IdEmpleado).Select(e => new
               { 
                   e.IdEmpleado,
                   e.Usuario,
                   e.Contraseña,
                   e.Rol,
                   Estado = new
                   { 
                       e.FkIdEstadoNavigation.IdEstado,
                       e.FkIdEstadoNavigation.Nombre
                   }

               }).FirstOrDefaultAsync();
               if (Empleado == null)
               {
                   return NotFound();
               }
               return Ok(Empleado);
           }
        */
        [HttpGet]
        [Route("{RolEmpleado}")]
        public async Task<IActionResult> RolEmpleado([FromRoute] string RolEmpleado)
        {
            var Empleado = await dbContext.TblEmpleados.Where(e => e.Usuario == RolEmpleado).Select(e => new
            {
                e.Usuario,
                e.Rol,
            }).FirstOrDefaultAsync();
            if (Empleado == null)
            {
                return NotFound();
            }
            return Ok(Empleado);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmpleado(EmpleadosRequest addEmpleado)
        {
            var Empleado = new TblEmpleados()

            {
                IdEmpleado = dbContext.TblEmpleados.Max(x => x.IdEmpleado) + 1,
                Usuario = addEmpleado.Usuario,
                Contraseña = Encrypt.GetSHA256(addEmpleado.Contraseña),
                Rol = addEmpleado.Rol,
                FkIdEstado = addEmpleado.FkIdEstado
            };

            await dbContext.TblEmpleados.AddAsync(Empleado);
            await dbContext.SaveChangesAsync();

            return Ok(Empleado);
        }
        [HttpPut]
        [Route("{IdEmpleado}")]
        public async Task<IActionResult> UpdateEmpleado([FromRoute] int IdEmpleado, EmpleadosRequest updateEmpleadoRequest)
        {
            var Empleado = await dbContext.TblEmpleados.FindAsync(IdEmpleado);

            if (Empleado != null)
            {
                Empleado.Usuario = updateEmpleadoRequest.Usuario;
                Empleado.Contraseña = Encrypt.GetSHA256(updateEmpleadoRequest.Contraseña);
                Empleado.Rol = updateEmpleadoRequest.Rol;
                Empleado.FkIdEstado = updateEmpleadoRequest.FkIdEstado;

                await dbContext.SaveChangesAsync();

                return Ok(Empleado);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{IdEmpleado}")]
        public async Task<IActionResult> DeleteEmpleado([FromRoute] int IdEmpleado)
        {
            var Empleado = await dbContext.TblEmpleados.FindAsync(IdEmpleado);
            if (Empleado != null)
            {
                dbContext.Remove(Empleado);
                await dbContext.SaveChangesAsync();
                return Ok(Empleado);
            }
            return NotFound();
        }
    }
}
