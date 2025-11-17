namespace SistemaAdministracao.Dtos
{
    public class PermissaoCreateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int IdSistema { get; set; }
    }
}
