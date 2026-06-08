using System;
using System.Collections.Generic;

namespace ReHope.Models;

public partial class Categorium
{
    public int CategoriaId { get; set; }

    public string NomeCategoria { get; set; } = null!;

    public int TipoProdutoId { get; set; }

    public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();

    public virtual TipoProduto TipoProduto { get; set; } = null!;
}
