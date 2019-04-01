using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IManage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IManage.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class ClientsController : ControllerBase
        {
            private readonly BankContext _context;

            public ClientsController(BankContext context)
            {
                _context = context;
                if (_context.Client.Count() == 0)
                {
            //    _context.Client.Add(new Client { FIO = "Ivanov Ivan Ivanovich", Value = 0 });
                    _context.SaveChanges();
                }
            }

            [HttpGet]
            public IEnumerable<Client> GetAll()
            {
                return _context.Client.Include(p => p.Investments);
        }

            [HttpGet("{id}", Name = "GetClient")]
            public async Task<IActionResult> GetClient([FromRoute] int id)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var client = await _context.Client.SingleOrDefaultAsync(m => m.ClientId == id);

                if (client == null)
                {
                    return NotFound();
                }

                return Ok(client);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] Client client)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Client.Add(client);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetClient", new { id = client.ClientId }, client);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Client client)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var item = _context.Client.Find(id);
                if (item == null)
                {
                    return NotFound();
                }
                item.FIO = client.FIO;
                _context.Client.Update(item);
                await _context.SaveChangesAsync();
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete([FromRoute] int id)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var item = _context.Client.Find(id);
                if (item == null)
                {
                    return NotFound();
                }
                _context.Client.Remove(item);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
    
}
