using Microsoft.EntityFrameworkCore;
using ReHope.Contexts;
using ReHope.Domains;
using ReHope.Interfaces;

namespace ReHope.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ReHopeContext _context;

        public ProdutoRepository(ReHopeContext context)
        {
            _context = context;
        }

        public List<Produto> Listar()
        {
            List<Produto> produtos = _context.Produto.Include(produto => produto.Categoria)
                .Include(produto => produto.Usuario)
                .OrderBy(produto => produto.NomeProduto)
                .ToList();

            return produtos;
        }

        public Produto ObterPorId(Guid id)
        {
            Produto? produto = _context.Produto
                .Include(produtoDb => produtoDb.Categoria)
                .Include(produtoDb => produtoDb.Usuario)

                .FirstOrDefault(produtoDb => produtoDb.ProdutoID == id);

            return produto;
        }

        public Produto ObterPorCodigo(int codigo)
        {
            Produto? produto = _context.Produto
                .Include(produtoDb => produtoDb.Categoria)
                .Include(produtoDb => produtoDb.Usuario)

                .FirstOrDefault(produtoDb => produtoDb.Codigo == codigo);

            return produto;
        }

        public bool LocalizacaoExiste(int localizacaoId)
        {
            return _context.Localizacao.Any(localizacao => localizacao.LocalizacaoID == localizacaoId);
        }

        //talvez aqui precise da categoria
        public void Adicionar(Produto produto)
        {
            _context.Produto.Add(produto);
            _context.SaveChanges();
        }

        public void Atualizar(Produto produto)
        {
            Produto? produtoBanco = _context.Produto
                .Include(produto => produto.Categoria)
                .FirstOrDefault(produtoAux => produtoAux.ProdutoID == produto.ProdutoID);

            if (produtoBanco == null)
            {
                return;
            }

            produtoBanco.NomeProduto = produto.NomeProduto;
            produtoBanco.Preco = produto.Preco;
            produtoBanco.Descricao = produto.Descricao;
            produtoBanco.Codigo = produto.Codigo;
            produtoBanco.Tamanho = produto.Tamanho;

            if (produto.Imagem != null && produto.Imagem.Length > 0)
            {
                produtoBanco.Imagem = produto.Imagem;
            }

            if (produto.StatusProduto != false)
            {
                produtoBanco.StatusProduto = produto.StatusProduto;
            }

            produtoBanco.Categoria = produto.Categoria;
            produtoBanco.Localizacao = produto.Localizacao;
        }

        public void Remover(Guid id)
        {
            Produto? produto = _context.Produto.FirstOrDefault(produto => produto.ProdutoID == id);

            if (produto == null)
            {
                return;
            }

            _context.Produto.Remove(produto);
            _context.SaveChanges();
        }

        public Localizacao BuscarLocalizacaoPorNome(string nomeLocalizacao)
        {
            return _context.Localizacao.FirstOrDefault(localizacao => localizacao.NomeLocalizacao.ToLower() == nomeLocalizacao.ToLower());
        }
    } 
}
