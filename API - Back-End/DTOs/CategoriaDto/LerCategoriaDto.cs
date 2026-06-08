namespace ReHope.DTOs.CategoriaDto
{

    public class LerCategoriaDto
    {
        public int CategoriaID { get; set; }

        public string NomeCategoria { get; set; } = string.Empty;

        public int TipoProdutoID { get; set; }

        public string NomeTipo { get; set; } = string.Empty;
    }
}
