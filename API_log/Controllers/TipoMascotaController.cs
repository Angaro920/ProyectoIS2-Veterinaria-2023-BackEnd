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
    public class TipoMascotaController : Controller
    {
        public readonly AppDbContext dbContext;
        public TipoMascotaController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetTipoMascota()
        {
            var resp = await dbContext.TblTipoMascota.Select(e => new
            { 
                e.IdTipoMascota,
                e.Nombre
            }).FirstOrDefaultAsync();
            return Ok(resp);
        }
        [HttpGet]
        [Route("{IdTipoMascota}")]
        public async Task<IActionResult> GetTipoMascota([FromRoute] int IdTipoMascota)
        {
            var tipoMascota = await dbContext.TblTipoMascota.Where(e => e.IdTipoMascota == IdTipoMascota).Select(e => new
            { 
                e.IdTipoMascota,
                e.Nombre
            }).FirstOrDefaultAsync();
            if (tipoMascota == null)
            {
                return NotFound();
            }
            return Ok(tipoMascota);
        }
        [HttpPost]
        public async Task<IActionResult> AddTipoMascota(TipoMascotaRequest addtipoMascotaRequest)
        {
            var tipoMascota = new TblTipoMascota()
            {
                IdTipoMascota = dbContext.TblTipoMascota.Max(x => x.IdTipoMascota) + 1,
                Nombre = addtipoMascotaRequest.Nombre
            };

            await dbContext.TblTipoMascota.AddAsync(tipoMascota);
            await dbContext.SaveChangesAsync();

            return Ok(tipoMascota);
        }
        [HttpPut]
        [Route("{IdTipoMascota}")]
        public async Task<IActionResult> UpdateTipoMascota([FromRoute] int IdTipoMascota, TipoMascotaRequest updateTipoMascotaRequest)
        {
            var tipoMascota = await dbContext.TblTipoMascota.FindAsync(IdTipoMascota);

            if (tipoMascota != null)
            {
                tipoMascota.Nombre = updateTipoMascotaRequest.Nombre;

                await dbContext.SaveChangesAsync();

                return Ok(tipoMascota);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{IdTipoMascota}")]
        public async Task<IActionResult> DeleteTipoMascota([FromRoute] int IdTipoMascota)
        {
            var tipoMascota = await dbContext.TblTipoMascota.FindAsync(IdTipoMascota);
            if (tipoMascota != null)
            {
                dbContext.Remove(tipoMascota);
                await dbContext.SaveChangesAsync();
                return Ok(tipoMascota);
            }
            return NotFound();
        }
    }
}
