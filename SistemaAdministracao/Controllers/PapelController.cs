using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAdministracao.Data;
using SistemaAdministracao.Dtos;
using SistemaAdministracao.Models;

namespace SistemaAdministracao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PapelController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PapelController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Papel>>> GetAll()
        {
            var papeis = await _context.Papeis
                .Include(p => p.PapeisPermissoes)
                    .ThenInclude(pp => pp.Permissao)
                .Include(p => p.Sistema)
                .ToListAsync();

            return Ok(papeis);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Papel>> GetById(int id)
        {
            var papel = await _context.Papeis
                .Include(p => p.PapeisPermissoes)
                    .ThenInclude(pp => pp.Permissao)
                .Include(p => p.Sistema)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (papel == null)
                return NotFound();

            return Ok(papel);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Papel>> Create(PapelCreateDto dto)
        {
            var papel = new Papel
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                IdSistema = dto.IdSistema
            };

            _context.Papeis.Add(papel);
            await _context.SaveChangesAsync();

            if (dto.IdsPermissoes != null && dto.IdsPermissoes.Any())
            {
                foreach (var idPermissao in dto.IdsPermissoes)
                {
                    _context.PapeisPermissoes.Add(new PapelPermissao
                    {
                        IdPapel = papel.Id,
                        IdPermissao = idPermissao
                    });
                }

                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetById), new { id = papel.Id }, papel);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, PapelCreateDto dto)
        {
            var papel = await _context.Papeis
                .Include(p => p.PapeisPermissoes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (papel == null)
                return NotFound();

            papel.Nome = dto.Nome;
            papel.Descricao = dto.Descricao;
            papel.IdSistema = dto.IdSistema;

            _context.PapeisPermissoes.RemoveRange(papel.PapeisPermissoes);

            if (dto.IdsPermissoes != null && dto.IdsPermissoes.Any())
            {
                foreach (var idPermissao in dto.IdsPermissoes)
                {
                    _context.PapeisPermissoes.Add(new PapelPermissao
                    {
                        IdPapel = papel.Id,
                        IdPermissao = idPermissao
                    });
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var papel = await _context.Papeis
                .Include(p => p.PapeisPermissoes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (papel == null)
                return NotFound();

            _context.PapeisPermissoes.RemoveRange(papel.PapeisPermissoes);
            _context.Papeis.Remove(papel);

            await _context.SaveChangesAsync();
            return Ok(new { message = $"Papel {papel.Nome} foi deletado com sucesso" });
        }
    }
}
