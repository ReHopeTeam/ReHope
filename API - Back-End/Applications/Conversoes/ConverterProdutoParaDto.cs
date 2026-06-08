using ReHope.Domains;
using ReHope.DTOs.ProdutoDto;

namespace ReHope.Applications.Conversoes
{
    public class ConverterProdutoParaDto
    {
        public static LerProdutoDto ConverterParaDto(Produto produto)
        {
            return new LerProdutoDto()
            {
                ProdutoID = produto.ProdutoID,
                NomeProduto = produto.NomeProduto,
                Preco = produto.Preco,
                Descricao = produto.Descricao,
                Codigo = produto.Codigo,
                StatusProduto = produto.StatusProduto,
                CategoriaID = produto.CategoriaID,
                LocalizacaoID = produto.LocalizacaoID,
                Imagem = $"produto/{produto.ProdutoID}/imagem",
                UsuarioID = produto.UsuarioID,

            };
        }
    }
}
