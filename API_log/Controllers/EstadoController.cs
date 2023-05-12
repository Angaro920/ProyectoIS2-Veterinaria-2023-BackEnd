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
    public class EstadoController : Controller
    {
        public readonly AppDbContext dbContext;
        public EstadoController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetEstados()
        {
            return Ok(await dbContext.TblEstados.ToListAsync());
        }
        [HttpGet]
        [Route("{IdEstado}")]
        public async Task<IActionResult> GetEstado([FromRoute] int IdEstado)
        {
            var Estado = await dbContext.TblEstados.FindAsync(IdEstado);
            if (Estado == null)
            {
                return NotFound();
            }
            return Ok(Estado);
        }
        [HttpPost]
        public async Task<IActionResult> AddEstado(EstadoRequest addEstado)
        {
            var Estado = new TblEstados()
            {
                IdEstado = dbContext.TblEstados.Max(x => x.IdEstado) + 1,
                Nombre = addEstado.Nombre,
                
            };

            await dbContext.TblEstados.AddAsync(Estado);
            await dbContext.SaveChangesAsync();

            return Ok(Estado);
        }
        [HttpPut]
        [Route("{IdEstado}")]
        public async Task<IActionResult> UpdateEstado([FromRoute] int IdEstado, EstadoRequest updateEstadoRequest)
        {
            var Estado = await dbContext.TblEstados.FindAsync(IdEstado);

            if (Estado != null)
            {
                Estado.Nombre = updateEstadoRequest.Nombre;

                await dbContext.SaveChangesAsync();

                return Ok(Estado);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{IdEstado}")]
        public async Task<IActionResult> DeleteEstado([FromRoute] int IdEstado)
        {
            var Estado = await dbContext.TblEstados.FindAsync(IdEstado);
            if (Estado != null)
            {
                dbContext.Remove(Estado);
                await dbContext.SaveChangesAsync();
                return Ok(Estado);
            }
            return NotFound();
        }
    }
}
