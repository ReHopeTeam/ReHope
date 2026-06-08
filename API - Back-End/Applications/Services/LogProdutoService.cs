using ReHope.Controllers;
using ReHope.Domains;
using ReHope.DTOs.LogProdutoDto;
using ReHope.Exceptions;
using ReHope.Interfaces;

namespace ReHope.Applications.Services
{
    public class LogProdutoService
    {
        private readonly ILogProdutoRepository _repository;

        public LogProdutoService(ILogProdutoRepository repository)
        {
            _repository = repository;
        }

        public List<LerLogProdutoDto> Listar()
        {
            List<LogProduto> logProdutos = _repository.Listar();

            List<LerLogProdutoDto> logProdutoDtos = logProdutos.Select(logProdutos => new LerLogProdutoDto
            {
                LogProdutoID = logProdutos.LogProdutoID,
                NomeAnterior = logProdutos.NomeAnterior,
                PrecoAnterior = logProdutos.PrecoAnterior,
                StatusProduto = logProdutos.StatusProduto,
                ProdutoID = logProdutos.ProdutoID,
                Codigo = logProdutos.Codigo,
                LocalizacaoIDAnterior  = logProdutos.LocalizacaoIDAnterior,
                UsuarioID = logProdutos.UsuarioID,
            }).ToList();

            return logProdutoDtos;
        }

        public List<LerLogProdutoDto> BuscarLogProdutoPorPodutoId(Guid produtoId)
        {
            List<LogProduto> logProdutos = _repository.BuscarLogProdutoPorPodutoId(produtoId);

            if( logProdutos == null)
            {
                throw new DomainException("Produto não encontrado");
            }

            List<LerLogProdutoDto> logProdutoDtos = logProdutos.Select(logProdutos => new LerLogProdutoDto
            {
                LogProdutoID = logProdutos.LogProdutoID,
                NomeAnterior = logProdutos.NomeAnterior,
                PrecoAnterior = logProdutos.PrecoAnterior,
                StatusProduto = logProdutos.StatusProduto,
                ProdutoID = logProdutos.ProdutoID,
                Codigo = logProdutos.Codigo,
                LocalizacaoIDAnterior = logProdutos.LocalizacaoIDAnterior,
                UsuarioID = logProdutos.UsuarioID,
            }).ToList();

            return logProdutoDtos;
        }
    }
}
