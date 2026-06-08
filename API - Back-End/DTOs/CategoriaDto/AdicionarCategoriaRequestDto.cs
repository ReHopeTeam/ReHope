using ReHope.DTOs.TipoProdutoDto;

namespace ReHope.DTOs.CategoriaDto
{
    public class AdicionarCategoriaRequestDto
    {
        public CriarCategoriaDto Categoria { get; set; }
        public LerTipoProdutoDto TipoProduto { get; set; }
    }
}
