using System;
using System.Collections.Generic;

namespace ReHope.Domains;

public partial class Categoria
{
    public int CategoriaID { get; set; }

    public string NomeCategoria { get; set; } = null!;

    public int TipoProdutoID { get; set; }

    public virtual ICollection<Produto> Produto { get; set; } = new List<Produto>();

    public virtual TipoProduto TipoProduto { get; set; } = null!;
}
