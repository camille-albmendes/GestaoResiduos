using Microsoft.AspNetCore.Mvc;
using GestaoDeResiduos.Data;
using GestaoDeResiduos.Models;
using System.Threading.Tasks;

namespace GestaoDeResiduos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResidenciasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ResidenciasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Residencia>> PostResidencia(Residencia residencia)
        {
            _context.Residencias.Add(residencia);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetResidencia", new { id = residencia.Id }, residencia);
        }
    }
}
