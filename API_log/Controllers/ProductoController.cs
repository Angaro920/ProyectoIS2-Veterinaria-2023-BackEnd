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
    public class ProductoController : Controller
    {
        public readonly AppDbContext dbContext;
        public ProductoController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            return Ok(await dbContext.TblProductos.ToListAsync());
        }
        [HttpGet]
        [Route("{IdProducto}")]
        public async Task<IActionResult> GetProducto([FromRoute] int IdProducto)
        {
            var producto = await dbContext.TblProductos.FindAsync(IdProducto);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }
        [HttpPost]
        public async Task<IActionResult> AddProducto(ProductoRequest addProducto)
        {
            var producto = new TblProductos()
            {
                IdProducto = dbContext.TblTipoMascota.Max(x => x.IdTipoMascota) + 1,
                Nombre = addProducto.Nombre,
                Marca = addProducto.Marca,
                Descripcion = addProducto.Descripcion,
                Precio = addProducto.Precio,
                Exitencias = addProducto.Exitencias
            };

            await dbContext.TblProductos.AddAsync(producto);
            await dbContext.SaveChangesAsync();

            return Ok(producto);
        }
        [HttpPut]
        [Route("{IdProducto}")]
        public async Task<IActionResult> UpdateProducto([FromRoute] int IdProducto, ProductoRequest updateProductoRequest)
        {
            var producto = await dbContext.TblProductos.FindAsync(IdProducto);

            if (producto != null)
            {
                producto.Nombre = updateProductoRequest.Nombre;

                await dbContext.SaveChangesAsync();

                return Ok(producto);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{IdProducto}")]
        public async Task<IActionResult> DeleteProducto([FromRoute] int IdProducto)
        {
            var producto = await dbContext.TblTipoMascota.FindAsync(IdProducto);
            if (producto != null)
            {
                dbContext.Remove(producto);
                await dbContext.SaveChangesAsync();
                return Ok(producto);
            }
            return NotFound();
        }
    }
}
