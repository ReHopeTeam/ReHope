using Microsoft.EntityFrameworkCore;
using ReHope.Contexts;
using ReHope.Domains;
using ReHope.Interfaces;

namespace ReHope.Repository
{
    public class LogProdutoRepository : ILogProdutoRepository
    {
        private readonly ReHopeContext _context;
        public LogProdutoRepository(ReHopeContext context)
        {
            _context = context;
        }

        public List<LogProduto> Listar()
        {
            List<LogProduto> logProdutos = _context.LogProduto.OrderByDescending(logProduto => logProduto.DataAlteracao).ToList();

            return logProdutos;
        }
        public List<LogProduto> BuscarLogProdutoPorPodutoId(Guid produtoId)
        {

            List<LogProduto> logProdutoAlteracao = _context.LogProduto
                   .Where(logProduto => logProduto.ProdutoID == produtoId)
                   .OrderByDescending(logProduto => logProduto.DataAlteracao)
                   .ToList();
            return logProdutoAlteracao;

        }   
    }
}
