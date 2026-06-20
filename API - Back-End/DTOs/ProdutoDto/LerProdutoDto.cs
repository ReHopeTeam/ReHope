using ReHope.Domains;

namespace ReHope.DTOs.ProdutoDto
{
    public class LerProdutoDto
    {
        public Guid ProdutoID { get; set; }
        public string NomeProduto { get; set; } = null!;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = null!;
        public int Codigo { get; set; }
        public string? Tamanho { get; set; }
        public string? Imagem { get; set; }
        public bool? StatusProduto { get; set; }
        public int CategoriaID { get; set; }
        public int LocalizacaoID { get; set; }
        public Guid UsuarioID { get; set; }
        public int TipoProdutoID { get; set; }
    }
}
