namespace SistemaAdministracao.Models
{
    public class PapelPermissao
    {
        public int IdPapel { get; set; }
        public Papel? Papel { get; set; }

        public int IdPermissao { get; set; }
        public Permissao? Permissao { get; set; }
    }
}
