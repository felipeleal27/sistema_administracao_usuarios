namespace SistemaAdministracao.Models
{
    public class Sistema
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }

        public ICollection<Permissao>? Permissoes { get; set; }
        public ICollection<Papel>? Papeis { get; set; }
    }
}
