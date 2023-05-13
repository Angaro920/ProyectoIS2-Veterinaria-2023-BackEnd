using API_Log.Context;
using API_Log.Models;
using API_Log.RequestModels;
using API_Log.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace API_Log.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MascotaController : Controller
    {
        public readonly AppDbContext dbContext;
        public MascotaController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetMascotas()
        {
            var resp = await dbContext.TblMascotas
                .Select(m => new
                {
                    m.IdMascota,
                    m.Nombre,
                    Estado = new
                    {
                        m.FkIdEstadoNavigation.IdEstado,
                        m.FkIdEstadoNavigation.Nombre
                    },
                    TipoMascota = new
                    {
                        m.FkIdTipoMascotaNavigation.IdTipoMascota,
                        m.FkIdTipoMascotaNavigation.Nombre
                    },
                    Raza = new 
                    {
                        m.FkIdRazaNavigation.IdRaza,
                        m.FkIdRazaNavigation.Nombre
                    }
                })
                .ToListAsync();

            return Ok(resp);
        }
        [HttpGet]
        [Route("{IdMascota}")]
        public async Task<IActionResult> GetMascota([FromRoute] int IdMascota)
        {
            var Mascota = await dbContext.TblMascotas.Where(e=> e.IdMascota== IdMascota).Select(e => new 
            {
                e.IdMascota,
                e.Nombre,
                Estado = new
                {
                    e.FkIdEstadoNavigation.IdEstado,
                    e.FkIdEstadoNavigation.Nombre
                },
                TipoMascota = new
                {
                    e.FkIdTipoMascotaNavigation.IdTipoMascota,
                    e.FkIdTipoMascotaNavigation.Nombre
                },
                Raza = new
                {
                    e.FkIdRazaNavigation.IdRaza,
                    e.FkIdRazaNavigation.Nombre
                }
            }).FirstOrDefaultAsync();
            if (Mascota == null)
            {
                return NotFound();
            }
            return Ok(Mascota);
        }
        [HttpPost]
        public async Task<IActionResult> AddMascota(MascotaRequest addMascota)
        {
            var Mascota = new TblMascotas()

            {
                IdMascota = dbContext.TblMascotas.Max(x => x.IdMascota) + 1,
                Nombre = addMascota.Nombre,
                FkIdCliente = addMascota.FkIdCliente,
                FkIdTipoMascota = addMascota.FkIdTipoMascota,
                FkIdRaza = addMascota.FkIdRaza,
                FkIdEstado = addMascota.FkIdRaza,
                
            };

            await dbContext.TblMascotas.AddAsync(Mascota);
            await dbContext.SaveChangesAsync();

            return Ok(Mascota);
        }
        [HttpPut]
        [Route("{IdMascota}")]
        public async Task<IActionResult> UpdateMascota([FromRoute] int IdMascota, MascotaRequest updateMascotaRequest)
        {
            var Mascota = await dbContext.TblMascotas.FindAsync(IdMascota);

            if (Mascota != null)
            {
                Mascota.Nombre = updateMascotaRequest.Nombre;
                Mascota.FkIdCliente = updateMascotaRequest.FkIdCliente;
                Mascota.FkIdTipoMascota = updateMascotaRequest.FkIdTipoMascota;
                Mascota.FkIdRaza = updateMascotaRequest.FkIdRaza;
                Mascota.FkIdEstado = updateMascotaRequest.FkIdEstado;

                await dbContext.SaveChangesAsync();

                return Ok(Mascota);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{IdMascota}")]
        public async Task<IActionResult> DeleteMascota([FromRoute] int IdMascota)
        {
            var Mascota = await dbContext.TblMascotas.FindAsync(IdMascota);
            if (Mascota != null)
            {
                dbContext.Remove(Mascota);
                await dbContext.SaveChangesAsync();
                return Ok(Mascota);
            }
            return NotFound();
        }
    }
}
