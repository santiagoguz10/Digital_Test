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
    public class ClienteController : ControllerBase
    {
        private readonly ApiContext _context;

        public ClienteController(ApiContext context)
        {
            _context = context;

        }

        #region Listar Clientes

        [HttpGet]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cliente))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        #endregion

        #region Listar Cliente Id

        [HttpGet("{id}")]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cliente))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Cliente>> GetClienteId(int id)
        {
            var solicitudItem = await _context.Clientes.FindAsync(id);

            if (solicitudItem == null)
            {
                return NotFound();
            }

            return solicitudItem;
        }
        #endregion

        #region Crear Cliente


        [HttpPost]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cliente))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cliente>> PostCrearCliente(Cliente item)
        {
            _context.Clientes.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClienteId), new { id = item.Id_Cliente }, item);
        }

        #endregion

        #region Validar si existe la Cliente
        private bool ExistCliente(long id) =>
         _context.Clientes.Any(e => e.Id_Cliente == id);
        #endregion

        #region Actualizar Cliente

        
        [HttpPut("{id}")]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cliente))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTodoItem(int id, Cliente item)
        {
            if (id != item.Id_Cliente)
            {
                return BadRequest();
            }

            var todoItem = await _context.Clientes.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            
            item.Nombre = item.Nombre;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ExistCliente(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        #endregion


        #region Eliminar Cliente
        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var ClientesItem = await _context.Clientes.FindAsync(id);
            if (ClientesItem == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(ClientesItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

    }
}
