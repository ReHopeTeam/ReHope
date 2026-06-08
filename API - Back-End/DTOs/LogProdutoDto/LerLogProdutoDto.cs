namespace ReHope.DTOs.LogProdutoDto
{
    public class LerLogProdutoDto
    {
        public Guid LogProdutoID { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string NomeAnterior { get; set; } = string.Empty;
        public decimal PrecoAnterior { get; set; }
        public Boolean StatusProduto { get; set; }
        public Guid ProdutoID { get; set; }
        public int Codigo { get; set; }
        public int LocalizacaoIDAnterior { get; set; }
        public Guid UsuarioID { get; set; }
    }
}
