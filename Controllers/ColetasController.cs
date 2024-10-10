using Microsoft.AspNetCore.Mvc;
using GestaoDeResiduos.Data;
using GestaoDeResiduos.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeResiduos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColetasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ColetasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("ProximaColeta/{logradouro}")]
        public async Task<ActionResult<string>> GetProximaColeta(string logradouro)
        {
            var residencia = await _context.Residencias
                .Where(r => r.Logradouro == logradouro && r.LixoParaColeta)
                .OrderByDescending(r => r.DataProximaColeta)
                .FirstOrDefaultAsync();

            if (residencia == null)
                return NotFound("Não há residências com lixo para coleta neste logradouro.");

            return Ok($"Próxima coleta será em: {residencia.DataProximaColeta}");
        }
    }
}
