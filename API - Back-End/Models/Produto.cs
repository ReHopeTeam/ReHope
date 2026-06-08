using System;
using System.Collections.Generic;

namespace ReHope.Models;

public partial class Produto
{
    public Guid ProdutoId { get; set; }

    public string NomeProduto { get; set; } = null!;

    public decimal Preco { get; set; }

    public string Descricao { get; set; } = null!;

    public int Codigo { get; set; }

    public string? Tamanho { get; set; }

    public string? Imagem { get; set; }

    public bool StatusProduto { get; set; }

    public int CategoriaId { get; set; }

    public int LocalizacaoId { get; set; }

    public Guid UsuarioId { get; set; }

    public virtual Categorium Categoria { get; set; } = null!;

    public virtual Localizacao Localizacao { get; set; } = null!;

    public virtual ICollection<LogProduto> LogProdutoCodigoNavigations { get; set; } = new List<LogProduto>();

    public virtual ICollection<LogProduto> LogProdutoProdutos { get; set; } = new List<LogProduto>();

    public virtual Usuario Usuario { get; set; } = null!;
}
