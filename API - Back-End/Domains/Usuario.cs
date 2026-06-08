using System;
using System.Collections.Generic;

namespace ReHope.Domains;

public partial class Usuario
{
    public Guid UsuarioID { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] Senha { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public bool? StatusUsuario { get; set; }

    public virtual ICollection<LogProduto> LogProduto { get; set; } = new List<LogProduto>();

    public virtual ICollection<Produto> Produto { get; set; } = new List<Produto>();
}
