namespace SistemaAdministracao.Dtos
{
    public class UsuarioLoginResponseDto
    {
        public string Token { get; set; } = null!;
        public List<String> Permissoes { get; set; } = new();
    }
}
