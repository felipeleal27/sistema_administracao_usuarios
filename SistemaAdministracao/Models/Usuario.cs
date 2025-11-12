namespace SistemaAdministracao.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;

        public ICollection<UsuarioPapel>? UsuariosPapeis { get; set; }
    }
}
