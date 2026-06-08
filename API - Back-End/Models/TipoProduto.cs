using System;
using System.Collections.Generic;

namespace ReHope.Models;

public partial class TipoProduto
{
    public int TipoProdutoId { get; set; }

    public string NomeTipo { get; set; } = null!;

    public virtual ICollection<Categorium> Categoria { get; set; } = new List<Categorium>();
}
