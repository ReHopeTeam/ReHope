namespace ReHope.DTOs.TipoProdutoDto
{
    public class LerTipoProdutoDto
    {
        public int TipoId { get; set; }
        public string NomeTipo { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public string NomeCategoria { get; set; } = string.Empty;
    }
}