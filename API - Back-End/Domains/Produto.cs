using System;
using System.Collections.Generic;

namespace ReHope.Domains;

public partial class Produto
{
    public Guid ProdutoID { get; set; }

    public string NomeProduto { get; set; } = null!;

    public decimal Preco { get; set; }

    public string Descricao { get; set; } = null!;

    public int Codigo { get; set; }

    public string? Tamanho { get; set; }

    public string? Imagem { get; set; }

    public bool StatusProduto { get; set; }

    public int CategoriaID { get; set; }

    public int LocalizacaoID { get; set; }

    public Guid UsuarioID { get; set; }

    public virtual Categoria Categoria { get; set; } = null!;

    public virtual Localizacao Localizacao { get; set; } = null!;

    public virtual ICollection<LogProduto> LogProdutoCodigoNavigation { get; set; } = new List<LogProduto>();

    public virtual ICollection<LogProduto> LogProdutoProduto { get; set; } = new List<LogProduto>();

    public virtual Usuario Usuario { get; set; } = null!;
}
