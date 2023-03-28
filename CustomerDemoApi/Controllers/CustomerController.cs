using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerDemoApi.Models;

namespace CustomerDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomerController(CustomerContext context)
        {
            _context = context;
        }

        // GET: api/DNIs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetDNIs()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/DNIs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetDNI(int id)
        {
            var dNI = await _context.Customers.FindAsync(id);

            if (dNI == null)
            {
                return NotFound();
            }

            return dNI;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDNI(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DNIExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Customer>> PostDNI(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDNI", new { id = customer.Id }, customer);
        }

        // DELETE: api/DNIs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteDNI(int id)
        {
            var dNI = await _context.Customers.FindAsync(id);
            if (dNI == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(dNI);
            await _context.SaveChangesAsync();

            return dNI;
        }

        private bool DNIExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
