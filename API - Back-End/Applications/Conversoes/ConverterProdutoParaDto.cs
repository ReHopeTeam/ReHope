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
        
                Tamanho = produto.Tamanho,
        
                StatusProduto = produto.StatusProduto,
                CategoriaID = produto.CategoriaID,
                LocalizacaoID = produto.LocalizacaoID,
                Imagem = produto.Imagem,
                UsuarioID = produto.UsuarioID,
        
                TipoProdutoID = produto.Categoria.TipoProdutoID
            };
        }
    }
}
