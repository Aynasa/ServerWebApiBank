using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IManage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IManage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgController : ControllerBase
    {
        private readonly BankContext _context;

        public ProgController(BankContext context)
        {
            _context = context;
            if (_context.Prog.Count() == 0)
            {
              //  _context.Prog.Add(new Prog { percent = 2, Name = "Невыгодный", months = 12 });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Prog> GetAll()
        {
            return _context.Prog.Include(p => p.Investments); ;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prog = await _context.Prog.SingleOrDefaultAsync(m => m.ProgId == id);

            if (prog == null)
            {
                return NotFound();
            }

            return Ok(prog);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Prog prog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Prog.Add(prog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = prog.ProgId }, prog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Prog prog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _context.Prog.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Name = prog.Name;
            item.percent = prog.percent;
            _context.Prog.Update(item);
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
            var item = _context.Prog.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Prog.Remove(item);
                    
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
