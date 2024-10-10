using Microsoft.AspNetCore.Mvc;
using GestaoDeResiduos.Data;
using GestaoDeResiduos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoDeResiduos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResiduosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ResiduosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resíduo>>> GetResiduos()
        {
            return await _context.Resíduos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Resíduo>> PostResíduo(Resíduo resíduo)
        {
            _context.Resíduos.Add(resíduo);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetResíduo", new { id = resíduo.Id }, resíduo);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Resíduo>> GetResíduo(int id)
        {
            var resíduo = await _context.Resíduos.FindAsync(id);
            if (resíduo == null)
                return NotFound();

            return resíduo;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResíduo(int id)
        {
            var resíduo = await _context.Resíduos.FindAsync(id);
            if (resíduo == null)
                return NotFound();

            _context.Resíduos.Remove(resíduo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
