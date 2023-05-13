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
    public class HistoriaClinicaController : Controller
    {
        public readonly AppDbContext dbContext;
        public HistoriaClinicaController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetHistoriaClinicas()
        {
            var resp = await dbContext.TblHistoriaClinica.Select(e => new
            {
                e.IdHistoriaClinica,
                e.Fecha,
                e.Diagnostico,
                e.OrdenMedica,
                Empleado = new 
                {
                    e.FkIdEmpleadoNavigation.IdEmpleado,
                    e.FkIdEmpleadoNavigation.Usuario
                }
            }).ToListAsync();
            return Ok(resp);
        }
        [HttpGet]
        [Route("{IdHistoriaClinica}")]
        public async Task<IActionResult> GetHistoriaClinica([FromRoute] int IdHistoriaClinica)
        {
            var HistoriaClinica = await dbContext.TblHistoriaClinica.Where(e => e.IdHistoriaClinica == IdHistoriaClinica).Select(e => new
            {
                e.Fecha,
                e.Diagnostico,
                e.OrdenMedica,
                Empleado = new
                {
                    e.FkIdEmpleadoNavigation.IdEmpleado,
                    e.FkIdEmpleadoNavigation.Usuario
                }
            }).FirstOrDefaultAsync();
            if (HistoriaClinica == null)
            {
                return NotFound();
            }
            return Ok(HistoriaClinica);
        }
        [HttpPost]
        public async Task<IActionResult> AddHistoriaClinica(HistoriaClinicaRequest addHistoriaClinica)
        {
            var HistoriaClinica = new TblHistoriaClinica()
            {
                IdHistoriaClinica = dbContext.TblHistoriaClinica.Max(x => x.IdHistoriaClinica) + 1,
                Fecha = addHistoriaClinica.Fecha,
                Diagnostico = addHistoriaClinica.Diagnostico,
                OrdenMedica = addHistoriaClinica.OrdenMedica,
                FkIdEmpleado = addHistoriaClinica.FkIdEmpleado

            };

            await dbContext.TblHistoriaClinica.AddAsync(HistoriaClinica);
            await dbContext.SaveChangesAsync();

            return Ok(HistoriaClinica);
        }
        [HttpPut]
        [Route("{IdHistoriaClinica}")]
        public async Task<IActionResult> UpdateHistoriaClinica([FromRoute] int IdHistoriaClinica, HistoriaClinicaRequest updateHistoriaClinicaRequest)
        {
            var HistoriaClinica = await dbContext.TblHistoriaClinica.FindAsync(IdHistoriaClinica);

            if (HistoriaClinica != null)
            {
                HistoriaClinica.Fecha = updateHistoriaClinicaRequest.Fecha;
                HistoriaClinica.Diagnostico = updateHistoriaClinicaRequest.Diagnostico;
                HistoriaClinica.OrdenMedica = updateHistoriaClinicaRequest.OrdenMedica;
                HistoriaClinica.FkIdEmpleado = updateHistoriaClinicaRequest.FkIdEmpleado;

                await dbContext.SaveChangesAsync();

                return Ok(HistoriaClinica);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{IdHistoriaClinica}")]
        public async Task<IActionResult> DeleteHistoriaClinica([FromRoute] int IdHistoriaClinica)
        {
            var HistoriaClinica = await dbContext.TblHistoriaClinica.FindAsync(IdHistoriaClinica);
            if (HistoriaClinica != null)
            {
                dbContext.Remove(HistoriaClinica);
                await dbContext.SaveChangesAsync();
                return Ok(HistoriaClinica);
            }
            return NotFound();
        }
    }
}
