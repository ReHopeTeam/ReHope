using System;
using System.Collections.Generic;

namespace ReHope.Models;

public partial class LogProduto
{
    public Guid LogProdutoId { get; set; }

    public DateTime DataAlteracao { get; set; }

    public string NomeAnterior { get; set; } = null!;

    public decimal PrecoAnterior { get; set; }

    public bool StatusProduto { get; set; }

    public Guid ProdutoId { get; set; }

    public int Codigo { get; set; }

    public int LocalizacaoIdanterior { get; set; }

    public Guid UsuarioId { get; set; }

    public virtual Produto CodigoNavigation { get; set; } = null!;

    public virtual Localizacao LocalizacaoIdanteriorNavigation { get; set; } = null!;

    public virtual Produto Produto { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
