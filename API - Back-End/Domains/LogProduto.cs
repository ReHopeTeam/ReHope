using System;
using System.Collections.Generic;

namespace ReHope.Domains;

public partial class LogProduto
{
    public Guid LogProdutoID { get; set; }

    public DateTime DataAlteracao { get; set; }

    public string NomeAnterior { get; set; } = null!;

    public decimal PrecoAnterior { get; set; }

    public bool StatusProduto { get; set; }

    public Guid ProdutoID { get; set; }

    public int Codigo { get; set; }

    public int LocalizacaoIDAnterior { get; set; }

    public Guid UsuarioID { get; set; }

    public virtual Produto CodigoNavigation { get; set; } = null!;

    public virtual Localizacao LocalizacaoIDAnteriorNavigation { get; set; } = null!;

    public virtual Produto Produto { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
