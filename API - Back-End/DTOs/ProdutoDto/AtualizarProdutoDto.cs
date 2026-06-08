namespace ReHope.DTOs.ProdutoDto
{
    public class AtualizarProdutoDto
    {
        public string NomeProduto { get; set; } = null!;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = null!;
        public int Codigo { get; set; }
        public string? Tamanho { get; set; }
        public IFormFile Imagem { get; set; } = null!;
        public bool? StatusProduto { get; set; }
        public int CategoriaID { get; set; }
        public int LocalizacaoID { get; set; }
    }
}
