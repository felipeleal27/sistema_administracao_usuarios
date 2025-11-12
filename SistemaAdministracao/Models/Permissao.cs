namespace SistemaAdministracao.Models
{
    public class Permissao
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }

        public int IdSistema { get; set; }
        public Sistema? Sistema { get; set; }

        public ICollection<PapelPermissao>? PapeisPermissoes { get; set; }
    }
}
