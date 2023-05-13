using API_Log.Context;
using API_Log.Models;
using API_Log.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace API_Log.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteHistoriaMascotaController : Controller
    {
        public readonly AppDbContext dbContext;
        public ClienteHistoriaMascotaController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetClienteHistoriaMascotas()
        {
            var resp = await dbContext.TblClienteHistoriaMascota.Select(e => new
            {
                e.IdClienteHistoriaMascota,
                Cliente = new 
                { 
                    e.FkIdClienteNavigation.Nombre
                },
                Mascota = new
                { 
                    e.FkIdMascotaNavigation.Nombre
                },
                Historia = new
                { 
                    e.FkIdHistoriaNavigation.Diagnostico,
                    e.FkIdHistoriaNavigation.OrdenMedica
                }
            }).ToListAsync();
            return Ok(resp);
        }
        [HttpGet]
        [Route("{IdClienteHistoriaMascota}")]
        public async Task<IActionResult> GetClienteHistoriaMascota([FromRoute] int IdClienteHistoriaMascota)
        {
            var ClienteHistoriaMascota = await dbContext.TblClienteHistoriaMascota.Where(e => e.IdClienteHistoriaMascota == IdClienteHistoriaMascota).Select(e => new
            {
                e.IdClienteHistoriaMascota,
                Cliente = new
                {
                    e.FkIdClienteNavigation.Nombre
                },
                Mascota = new
                {
                    e.FkIdMascotaNavigation.Nombre
                },
                Historia = new
                {
                    e.FkIdHistoriaNavigation.Diagnostico,
                    e.FkIdHistoriaNavigation.OrdenMedica
                }
            }).FirstOrDefaultAsync();
            if (ClienteHistoriaMascota == null)
            {
                return NotFound();
            }
            return Ok(ClienteHistoriaMascota);
        }
        [HttpPost]
        public async Task<IActionResult> AddClienteHistoriaMascota(ClienteHistoriaMascotaRequest addClienteHistoriaMascota)
        {
            var ClienteHistoriaMascota = new TblClienteHistoriaMascota()
            {
                IdClienteHistoriaMascota = dbContext.TblClienteHistoriaMascota.Max(x => x.IdClienteHistoriaMascota) + 1,
                FkIdCliente = addClienteHistoriaMascota.FkIdCliente,
                FkIdMascota = addClienteHistoriaMascota.FkIdMascota,
                FkIdHistoria = addClienteHistoriaMascota.FkIdHistoria

            };

            await dbContext.TblClienteHistoriaMascota.AddAsync(ClienteHistoriaMascota);
            await dbContext.SaveChangesAsync();

            return Ok(ClienteHistoriaMascota);
        }
        [HttpPut]
        [Route("{IdClienteHistoriaMascota}")]
        public async Task<IActionResult> UpdateClienteHistoriaMascota([FromRoute] int IdClienteHistoriaMascota, ClienteHistoriaMascotaRequest updateClienteHistoriaMascotaRequest)
        {
            var ClienteHistoriaMascota = await dbContext.TblClienteHistoriaMascota.FindAsync(IdClienteHistoriaMascota);

            if (ClienteHistoriaMascota != null)
            {
                ClienteHistoriaMascota.FkIdCliente = updateClienteHistoriaMascotaRequest.FkIdCliente;
                ClienteHistoriaMascota.FkIdMascota = updateClienteHistoriaMascotaRequest.FkIdMascota;
                ClienteHistoriaMascota.FkIdHistoria = updateClienteHistoriaMascotaRequest.FkIdHistoria;
                await dbContext.SaveChangesAsync();

                return Ok(ClienteHistoriaMascota);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{IdClienteHistoriaMascota}")]
        public async Task<IActionResult> DeleteClienteHistoriaMascota([FromRoute] int IdClienteHistoriaMascota)
        {
            var ClienteHistoriaMascota = await dbContext.TblClienteHistoriaMascota.FindAsync(IdClienteHistoriaMascota);
            if (ClienteHistoriaMascota != null)
            {
                dbContext.Remove(ClienteHistoriaMascota);
                await dbContext.SaveChangesAsync();
                return Ok(ClienteHistoriaMascota);
            }
            return NotFound();
        }
    }
}
