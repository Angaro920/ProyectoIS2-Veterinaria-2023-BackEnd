using API_Log.Models;
using API_Log.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using API_Log.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API_Log.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RazaController : Controller
    {
       
        public readonly AppDbContext dbContext;
        public RazaController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetRaza()
        {
            return Ok(await dbContext.TblRaza.ToListAsync());
        }

        [HttpGet]
        [Route("{IdRaza}")]
        public async Task<IActionResult> GetRaza([FromRoute] int IdRaza)
        {
            var raza = await dbContext.TblRaza.FindAsync(IdRaza);
            if (raza == null)
            {
                return NotFound();
            }
            return Ok(raza);
        }

        [HttpPost]
        public async Task<IActionResult> AddRaza(RazaRequest addRazaRequest)
        {
            var raza = new TblRaza()
            {
                IdRaza = dbContext.TblRaza.Max(x => x.IdRaza) + 1,
                Nombre = addRazaRequest.Nombre
            };

            await dbContext.TblRaza.AddAsync(raza);
            await dbContext.SaveChangesAsync();

            return Ok(raza);
        }
        [HttpPut]
        [Route("{IdRaza}")]
        public async Task<IActionResult> UpdateRaza([FromRoute] int IdRaza, RazaRequest updateRazaRequest)
        {
            var raza = await dbContext.TblRaza.FindAsync(IdRaza);

            if (raza != null)
            {
                raza.Nombre = updateRazaRequest.Nombre;

                await dbContext.SaveChangesAsync();

                return Ok(raza);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{IdRaza}")]
        public async Task<IActionResult> DeleteRaza([FromRoute] int IdRaza)
        {
            var raza = await dbContext.TblRaza.FindAsync(IdRaza);
            if (raza != null)
            {
                dbContext.Remove(raza);
                await dbContext.SaveChangesAsync();
                return Ok(raza);
            }
            return NotFound();
        }
    }
}
