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
            var resp = await dbContext.TblProductos
                 .Select(m => new
                 {
                     m.IdProducto,
                     m.Nombre,
                     m.Precio,
                     m.Marca,
                     m.Descripcion
                    
                 })
                 .ToListAsync();

            return Ok(resp);
        }
        [HttpGet]
        [Route("{IdProducto}")]
        public async Task<IActionResult> GetProducto([FromRoute] int IdProducto)
        {
            var producto = await dbContext.TblProductos.Where(e => e.IdProducto == IdProducto).Select(e => new
            {
                e.IdProducto,
                e.Nombre,
                e.Marca,
                e.Descripcion,
                e.Precio,
                e.Exitencias
            }).FirstOrDefaultAsync();
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
                IdProducto = dbContext.TblProductos.Max(x => x.IdProducto) + 1,
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
                producto.Marca = updateProductoRequest.Marca;
                producto.Descripcion = updateProductoRequest.Descripcion;
                producto.Precio = updateProductoRequest.Precio;
                producto.Exitencias = updateProductoRequest.Exitencias;
                producto.Exitencias = updateProductoRequest.Exitencias;

                await dbContext.SaveChangesAsync();

                return Ok(producto);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{IdProducto}")]
        public async Task<IActionResult> DeleteProducto([FromRoute] int IdProducto)
        {
            var producto = await dbContext.TblProductos.FindAsync(IdProducto);
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
