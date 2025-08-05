using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RH_Management.API.Models;
using RH_Management.API.Data;

namespace RH_Management.API.Controllers
{
    [ApiController]
    [Route("api/ferias")]
    public class FeriasController : ControllerBase
    {
        private readonly RHContext _context;

        public FeriasController(RHContext context)
        {
            _context = context;
        }

        // GET: api/ferias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ferias>>> GetAll()
        {
            return await _context.Ferias
                .Include(f => f.Funcionario)
                .ToListAsync();
        }

        // GET: api/ferias/funcionario/{funcionarioId}
        [HttpGet("funcionario/{funcionarioId}")]
        public async Task<ActionResult<IEnumerable<Ferias>>> GetByFuncionario(int funcionarioId)
        {
            return await _context.Ferias
                .Where(f => f.FuncionarioId == funcionarioId)
                .Include(f => f.Funcionario)
                .ToListAsync();
        }

        // GET: api/ferias/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Ferias>> GetById(int id)
        {
            var ferias = await _context.Ferias
                .Include(f => f.Funcionario)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (ferias == null) return NotFound();

            ferias.Status = CalcularStatusFerias(ferias.DataInicio, ferias.DataTermino);
            await _context.SaveChangesAsync();

            return ferias;
        }

        // POST: api/ferias
        [HttpPost]
        public async Task<ActionResult<Ferias>> Create([FromBody] Ferias ferias)
        {
            if (ferias.FuncionarioId <= 0)
                return BadRequest("ID do funcionário inválido");

            if (ferias.DataInicio >= ferias.DataTermino)
                return BadRequest("Data de término deve ser posterior à data de início");

            if (!await _context.Funcionarios.AnyAsync(f => f.Id == ferias.FuncionarioId))
                return BadRequest("Funcionário não encontrado");

            ferias.Status = CalcularStatusFerias(ferias.DataInicio, ferias.DataTermino);
            ferias.Funcionario = null;

            _context.Ferias.Add(ferias);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ferias.Id }, ferias);
        }

        // PUT: api/ferias/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Ferias ferias)
        {
            if (id != ferias.Id) return BadRequest();

            ferias.Status = CalcularStatusFerias(ferias.DataInicio, ferias.DataTermino);
            _context.Entry(ferias).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeriasExists(id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/ferias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ferias = await _context.Ferias.FindAsync(id);
            if (ferias == null) return NotFound();

            _context.Ferias.Remove(ferias);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeriasExists(int id)
        {
            return _context.Ferias.Any(e => e.Id == id);
        }

        private string CalcularStatusFerias(DateTime dataInicio, DateTime dataTermino)
        {
            var hoje = DateTime.Today;

            if (dataInicio > hoje) return "Pendente";
            if (dataTermino < hoje) return "Concluídas";
            return "Em andamento";
        }
    }
}