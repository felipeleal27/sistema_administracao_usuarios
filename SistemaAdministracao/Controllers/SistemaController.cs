using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAdministracao.Data;
using SistemaAdministracao.Dtos;
using SistemaAdministracao.Models;

namespace SistemaAdministracao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SistemaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SistemaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Sistema>>> GetAll()
        {
            var sistemas = await _context.Sistemas
                .Include(s => s.Permissoes)
                .Include(s => s.Papeis)
                .ToListAsync();

            return Ok(sistemas);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Sistema>> GetById(int id)
        {
            var sistema = await _context.Sistemas
                .Include(s => s.Permissoes)
                .Include(s => s.Papeis)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sistema == null)
                return NotFound();

            return Ok(sistema);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Sistema>> Create(SistemaCreateDto dto)
        {
            var sistema = new Sistema
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao
            };

            _context.Sistemas.Add(sistema);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = sistema.Id }, sistema);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, SistemaCreateDto dto)
        {
            var sistema = await _context.Sistemas.FindAsync(id);

            if (sistema == null)
                return NotFound();

            sistema.Nome = dto.Nome;
            sistema.Descricao = dto.Descricao;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sistema = await _context.Sistemas
                .Include(s => s.Permissoes)
                .Include(s => s.Papeis)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sistema == null)
                return NotFound();

            if (sistema.Permissoes != null)
            {
                _context.Permissoes.RemoveRange(sistema.Permissoes);
            }

            if (sistema.Papeis != null)
            {
                _context.Papeis.RemoveRange(sistema.Papeis);
            }

            _context.Sistemas.Remove(sistema);

            await _context.SaveChangesAsync();

            return Ok(new { message = $"Sistema '{sistema.Nome}' deletado com sucesso." });
        }
    }
}
