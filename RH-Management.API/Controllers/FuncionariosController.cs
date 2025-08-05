using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RH_Management.API.Models;
using RH_Management.API.Data;

namespace RH_Management.API.Controllers
{
    [ApiController]
    [Route("api/funcionarios")]
    public class FuncionariosController : ControllerBase
    {
        private readonly RHContext _context;

        public FuncionariosController(RHContext context)
        {
            _context = context;
        }

        // GET: api/funcionarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetAll()
        {
            return await _context.Funcionarios.ToListAsync();
        }

        // GET: api/funcionarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> GetById(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null) return NotFound();
            return funcionario;
        }

        // GET: api/funcionarios/por-cargo/{cargo}
        [HttpGet("por-cargo/{cargo}")]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetByCargo(string cargo)
        {
            return await _context.Funcionarios
                .Where(f => f.Cargo.ToLower() == cargo.ToLower())
                .ToListAsync();
        }

        // POST: api/funcionarios
        [HttpPost]
        public async Task<ActionResult<Funcionario>> Create([FromBody] Funcionario funcionario)
        {
            if (string.IsNullOrWhiteSpace(funcionario.Nome))
                return BadRequest("Nome é obrigatório");

            if (string.IsNullOrWhiteSpace(funcionario.Cargo))
                return BadRequest("Cargo é obrigatório");

            if (funcionario.Salario <= 0)
                return BadRequest("Salário deve ser maior que zero");

            funcionario.Ferias = null;
            funcionario.HistoricoAlteracoes = null;

            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = funcionario.Id }, funcionario);
        }

        // PUT: api/funcionarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Funcionario funcionario)
        {
            if (id != funcionario.Id) return BadRequest();

            var funcionarioExistente = await _context.Funcionarios.FindAsync(id);
            if (funcionarioExistente == null) return NotFound();

            RegistrarAlteracoes(funcionarioExistente, funcionario);
            _context.Entry(funcionarioExistente).CurrentValues.SetValues(funcionario);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // PATCH: api/funcionarios/{id}/status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] bool status)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null) return NotFound();

            funcionario.Status = status;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/funcionarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null) return NotFound();

            funcionario.Status = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.Id == id);
        }

        private void RegistrarAlteracoes(Funcionario original, Funcionario modificado)
        {
            var propriedades = typeof(Funcionario).GetProperties();
            foreach (var propriedade in propriedades)
            {
                var valorOriginal = propriedade.GetValue(original)?.ToString();
                var valorNovo = propriedade.GetValue(modificado)?.ToString();

                if (valorOriginal != valorNovo && propriedade.Name != "Id")
                {
                    _context.HistoricoAlteracoes.Add(new HistoricoAlteracao
                    {
                        FuncionarioId = original.Id,
                        CampoAlterado = propriedade.Name,
                        ValorAntigo = valorOriginal,
                        ValorNovo = valorNovo,
                        DataAlteracao = DateTime.Now
                    });
                }
            }
        }
    }
}