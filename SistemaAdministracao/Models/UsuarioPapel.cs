namespace SistemaAdministracao.Models
{
    public class UsuarioPapel
    {
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

        public int IdPapel { get; set; }
        public Papel? Papel { get; set; }
    }
}
