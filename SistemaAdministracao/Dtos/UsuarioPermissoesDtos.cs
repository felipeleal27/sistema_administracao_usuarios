public class UsuarioPermissoesDto
{
    public int IdUsuario { get; set; }
    public string NomeUsuario { get; set; } = string.Empty;
    public List<PermissaoSimples> Permissoes { get; set; } = new();
}

public class PermissaoSimples
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
}
