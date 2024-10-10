using Microsoft.AspNetCore.Mvc;
using GestaoDeResiduos.Data;
using GestaoDeResiduos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GestaoDeResiduos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LixoParaColetaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LixoParaColetaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SinalizarLixoParaColeta(int residenciaId)
        {
            var residencia = await _context.Residencias.FindAsync(residenciaId);
            if (residencia == null)
                return NotFound();

            residencia.LixoParaColeta = true;
            // Suponha que a coleta ocorra semanalmente
            residencia.DataProximaColeta = DateTime.Now.AddDays(7);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
