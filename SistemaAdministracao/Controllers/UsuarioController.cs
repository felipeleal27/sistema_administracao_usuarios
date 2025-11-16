using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAdministracao.Data;
using SistemaAdministracao.Dtos;
using SistemaAdministracao.Models;
using System.Linq;

namespace SistemaAdministracao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.UsuariosPapeis)
                    .ThenInclude(up => up.Papel)
                .ToListAsync();

            return Ok(usuarios);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.UsuariosPapeis)
                    .ThenInclude(up => up.Papel)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Usuario>> Create(UsuarioCreateDto dto)
        {
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            if (dto.IdsPapeis != null && dto.IdsPapeis.Any())
            {
                foreach (var idPapel in dto.IdsPapeis)
                {
                    _context.UsuariosPapeis.Add(new UsuarioPapel
                    {
                        IdUsuario = usuario.Id,
                        IdPapel = idPapel
                    });
                }

                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, UsuarioCreateDto dto)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.UsuariosPapeis)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
                return NotFound();

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;

            _context.UsuariosPapeis.RemoveRange(usuario.UsuariosPapeis ?? Enumerable.Empty<UsuarioPapel>());

            if (dto.IdsPapeis != null && dto.IdsPapeis.Any())
            {
                foreach (var idPapel in dto.IdsPapeis)
                {
                    _context.UsuariosPapeis.Add(new UsuarioPapel
                    {
                        IdUsuario = usuario.Id,
                        IdPapel = idPapel
                    });
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.UsuariosPapeis)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
                return NotFound();

            _context.UsuariosPapeis.RemoveRange(usuario.UsuariosPapeis ?? Enumerable.Empty<UsuarioPapel>());
            _context.Usuarios.Remove(usuario);

            await _context.SaveChangesAsync();
            return Ok(new { message = $"Usuário {usuario.Nome} foi deletado com sucesso." });
        }
    }
}
