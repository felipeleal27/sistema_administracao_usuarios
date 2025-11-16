using Microsoft.EntityFrameworkCore;
using SistemaAdministracao.Models;

namespace SistemaAdministracao.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Papel> Papeis { get; set; }
        public DbSet<Permissao> Permissoes { get; set; }
        public DbSet<Sistema> Sistemas { get; set; }
        public DbSet<UsuarioPapel> UsuariosPapeis { get; set; }
        public DbSet<PapelPermissao> PapeisPermissoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chaves compostas
            modelBuilder.Entity<UsuarioPapel>()
                .HasKey(up => new { up.IdUsuario, up.IdPapel });

            modelBuilder.Entity<PapelPermissao>()
                .HasKey(pp => new { pp.IdPapel, pp.IdPermissao });

            // Relacionamento Usuario <-> UsuarioPapel
            modelBuilder.Entity<UsuarioPapel>()
                .HasOne(up => up.Usuario)
                .WithMany(u => u.UsuariosPapeis)
                .HasForeignKey(up => up.IdUsuario);

            // Relacionamento Papel <-> UsuarioPapel
            modelBuilder.Entity<UsuarioPapel>()
                .HasOne(up => up.Papel)
                .WithMany(p => p.UsuariosPapeis)
                .HasForeignKey(up => up.IdPapel);

            // Relacionamento Papel <-> PapelPermissao
            modelBuilder.Entity<PapelPermissao>()
                .HasOne(pp => pp.Papel)
                .WithMany(p => p.PapeisPermissoes)
                .HasForeignKey(pp => pp.IdPapel);

            // Relacionamento Permissao <-> PapelPermissao
            modelBuilder.Entity<PapelPermissao>()
                .HasOne(pp => pp.Permissao)
                .WithMany(p => p.PapeisPermissoes)
                .HasForeignKey(pp => pp.IdPermissao);

            // Relacionamento Sistema <-> Papel  (QUE FALTAVA)
            modelBuilder.Entity<Papel>()
                .HasOne(p => p.Sistema)
                .WithMany(s => s.Papeis)
                .HasForeignKey(p => p.IdSistema)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
