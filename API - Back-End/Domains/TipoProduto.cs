using System;
using System.Collections.Generic;

namespace ReHope.Domains;

public partial class TipoProduto
{
    public int TipoProdutoID { get; set; }

    public string NomeTipo { get; set; } = null!;

    public virtual ICollection<Categoria> Categoria { get; set; } = new List<Categoria>();
}
