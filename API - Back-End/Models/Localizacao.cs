using System;
using System.Collections.Generic;

namespace ReHope.Models;

public partial class Localizacao
{
    public int LocalizacaoId { get; set; }

    public string NomeLocalizacao { get; set; } = null!;

    public virtual ICollection<LogProduto> LogProdutos { get; set; } = new List<LogProduto>();

    public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
