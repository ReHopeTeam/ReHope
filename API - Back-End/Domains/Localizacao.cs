using System;
using System.Collections.Generic;

namespace ReHope.Domains;

public partial class Localizacao
{
    public int LocalizacaoID { get; set; }

    public string NomeLocalizacao { get; set; } = null!;

    public virtual ICollection<LogProduto> LogProduto { get; set; } = new List<LogProduto>();

    public virtual ICollection<Produto> Produto { get; set; } = new List<Produto>();
}
