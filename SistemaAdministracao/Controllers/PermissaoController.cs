using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAdministracao.Data;
using SistemaAdministracao.Dtos;
using SistemaAdministracao.Models;

namespace SistemaAdministracao.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PermissaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PermissaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Permissao>>> GetAll()
        {
            var permissoes = await _context.Permissoes
                .Include(p => p.Sistema)
                .Include(p => p.PapeisPermissoes!)
                    .ThenInclude(pp => pp.Papel)
                .ToListAsync();

            return Ok(permissoes);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Permissao>> GetById(int id)
        {
            var permissao = await _context.Permissoes
                .Include(p => p.Sistema)
                .Include(p => p.PapeisPermissoes!)
                    .ThenInclude(pp => pp.Papel)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (permissao == null)
                return NotFound();

            return Ok(permissao);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<object>> Create(PermissaoCreateDto dto)
        {
            var permissao = new Permissao
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                IdSistema = dto.IdSistema
            };

            _context.Permissoes.Add(permissao);
            await _context.SaveChangesAsync();

            return Ok(permissao);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<object>> Update(int id, PermissaoCreateDto dto)
        {
            var permissao = await _context.Permissoes.FindAsync(id);

            if (permissao == null)
                return NotFound();

            permissao.Nome = dto.Nome;
            permissao.Descricao = dto.Descricao;
            permissao.IdSistema = dto.IdSistema;

            await _context.SaveChangesAsync();
            return Ok(permissao);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            var permissao = await _context.Permissoes.FindAsync(id);

            if (permissao == null)
                return NotFound();

            _context.Permissoes.Remove(permissao);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Permissão '{permissao.Nome}' removida." });
        }
    }
}
