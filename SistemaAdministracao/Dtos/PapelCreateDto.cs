namespace SistemaAdministracao.Dtos
{
    public class PapelCreateDto
    {
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }
        public int IdSistema { get; set; }
        public List<int>? IdsPermissoes { get; set; }
    }
}
