namespace ReHope.DTOs.ProdutoDto
{
    public class CriarProdutoDto
    {
        public string NomeProduto { get; set; } = null!;
        public decimal Preco { get; set; }
        public string? Descricao { get; set; }
        public string? Tamanho { get; set; }
        public IFormFile Imagem { get; set; }
        public int CategoriaID { get; set; }
        public int LocalizacaoID { get; set; }
    }
}
