using API_Log.Context;
using API_Log.Models;
using API_Log.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace API_Log.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        public readonly AppDbContext dbContext;
        public ClienteController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var resp = await dbContext.TblCliente.Select(m => new
            {
                m.IdCliente,
                m.Nombre,
                m.Cedula,
                m.Celular,
                m.Correo,
                Estado = new
                {
                    m.FkIdEstadoNavigation.IdEstado,
                    m.FkIdEstadoNavigation.Nombre
                }
            }).ToListAsync();
            return Ok(resp);
        }
        [HttpGet]
        [Route("{IdCliente}")]
        public async Task<IActionResult> GetCliente([FromRoute] int IdCliente)
        {
            var Cliente = await dbContext.TblCliente.FindAsync(IdCliente);
            if (Cliente == null)
            {
                return NotFound();
            }
            return Ok(Cliente);
        }
        [HttpPost]
        public async Task<IActionResult> AddCliente(ClienteRequest addCliente)
        {
            var Cliente = new TblCliente()
            {
                IdCliente = dbContext.TblCliente.Max(x => x.IdCliente) + 1,
                Nombre = addCliente.Nombre,
                Cedula = addCliente.Cedula,
                Correo = addCliente.Correo,
                Celular = addCliente.Celular,
                FkIdEstado = addCliente.FkIdEstado
            };

            await dbContext.TblCliente.AddAsync(Cliente);
            await dbContext.SaveChangesAsync();

            return Ok(Cliente);
        }
        [HttpPut]
        [Route("{IdCliente}")]
        public async Task<IActionResult> UpdateCliente([FromRoute] int IdCliente, ClienteRequest updateClienteRequest)
        {
            var Cliente = await dbContext.TblCliente.FindAsync(IdCliente);

            if (Cliente != null)
            {
                Cliente.Nombre = updateClienteRequest.Nombre;
                Cliente.Cedula = updateClienteRequest.Cedula;
                Cliente.Correo = updateClienteRequest.Correo;
                Cliente.Celular = updateClienteRequest.Cedula;
                Cliente.FkIdEstado = updateClienteRequest.FkIdEstado;

                await dbContext.SaveChangesAsync();

                return Ok(Cliente);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{IdCliente}")]
        public async Task<IActionResult> DeleteCliente([FromRoute] int IdCliente)
        {
            var Cliente = await dbContext.TblCliente.FindAsync(IdCliente);
            if (Cliente != null)
            {
                dbContext.Remove(Cliente);
                await dbContext.SaveChangesAsync();
                return Ok(Cliente);
            }
            return NotFound();
        }
    }
}
