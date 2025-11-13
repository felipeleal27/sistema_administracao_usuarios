namespace SistemaAdministracao.Dtos
{
    public class UsuarioCreateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<int> IdsPapeis { get; set; } = new();
    }
}
