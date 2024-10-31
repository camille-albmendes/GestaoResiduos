using Microsoft.AspNetCore.Mvc;
using GestaoDeResiduos.Data;

namespace GestaoDeResiduos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinalizarColetaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FinalizarColetaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPut("{residenciaId}")]
        public async Task<IActionResult> SinalizarColetaFinalizada(int residenciaId)
        {
            var residencia = await _context.Residencias.FindAsync(residenciaId);
            if (residencia == null)
                return NotFound();

            residencia.LixoParaColeta = false;
            residencia.DataProximaColeta = null;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
