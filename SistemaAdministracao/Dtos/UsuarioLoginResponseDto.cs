namespace SistemaAdministracao.Dtos
{
    public class UsuarioLoginResponseDto
    {
        public string Token { get; set; } = null!;
        public List<int> Papeis { get; set; } = new();
    }
}
