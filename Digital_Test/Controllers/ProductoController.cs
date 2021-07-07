using Digital_Test.Context;
using Digital_Test.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ApiContext _context;

        public ProductoController(ApiContext context)
        {
            _context = context;

        }

        #region Listar Productos

        [HttpGet]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Producto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        #endregion

        #region Listar Producto Id

        [HttpGet("{id}")]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Producto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Producto>> GetProductoId(int id)
        {
            var solicitudItem = await _context.Productos.FindAsync(id);

            if (solicitudItem == null)
            {
                return NotFound();
            }

            return solicitudItem;
        }
        #endregion

        #region Crear Producto


        [HttpPost]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Producto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> PostCrearProducto (Producto item)
        {
            _context.Productos.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductoId), new { id = item.Id_Producto }, item);
        }

        #endregion

        #region Validar si existe el producto
        private bool ExistProducto(long id) =>
         _context.Productos.Any(e => e.Id_Producto == id);
        #endregion

        #region Actualizar Factura

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(long id, Producto item)
        {
            if (id != item.Id_Producto)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExistProducto(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }

        #endregion


        #region Eliminar Producto
        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(long id)
        {
            var productoItem = await _context.Productos.FindAsync(id);
            if (productoItem == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(productoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
