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
    public class FacturaController : ControllerBase
    {
        private readonly ApiContext _context;

        public FacturaController(ApiContext context)
        {
            _context = context;

        }

        #region Listar Facturas

        [HttpGet]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Factura))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            return await _context.Facturas.ToListAsync();
        }

        #endregion

        #region Listar Factura Id

        [HttpGet("{id}")]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Factura))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Factura>> GetFacturaId(int id)
        {
            var solicitudItem = await _context.Facturas.FindAsync(id);

            if (solicitudItem == null)
            {
                return NotFound();
            }

            return solicitudItem;
        }
        #endregion

        #region Crear Factura


        [HttpPost]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Factura))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Factura>> PostCrearFactura(Factura item)
        {
            _context.Facturas.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFacturaId), new { id = item.Id_Factura }, item);
        }

        #endregion

        #region Validar si existe la factura
        private bool ExistFactura(long id) =>
         _context.Facturas.Any(e => e.Id_Factura == id);
        #endregion

        #region Actualizar Factura

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura(long id, Factura item)
        {
            if (id != item.Id_Factura)
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
                if (!ExistFactura(id))
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


        #region Eliminar Factura
        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(long id)
        {
            var facturasItem = await _context.Facturas.FindAsync(id);
            if (facturasItem == null)
            {
                return NotFound();
            }

            _context.Facturas.Remove(facturasItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

    }
}

