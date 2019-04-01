using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IManage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IManage.Controllers
{
    [Route("api/[controller]")]
    public class InvestmentController : Controller
    {

        private readonly BankContext _context;

        public InvestmentController(BankContext context)
        {
            _context = context;
            if (_context.Investment.Count() == 0)
            {
               // _context.Investment.Add(new Investment { Balance = 1234, ClientId = 1, ProgId = 1 });
                _context.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<Investment> GetAll()
        {
            return _context.Investment;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var investment = await _context.Investment.SingleOrDefaultAsync(m => m.InvestmentId == id);

            if (investment == null)
            {
                return NotFound();
            }

            return Ok(investment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Investment investment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Investment.Add(investment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = investment.InvestmentId }, investment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Investment investment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _context.Investment.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            //item.Name = prog.Name;
            //item.percent = prog.percent;
            _context.Investment.Update(item);
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
            var item = _context.Investment.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Investment.Remove(item);
           
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
