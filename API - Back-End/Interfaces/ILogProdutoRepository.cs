using ReHope.Domains;

namespace ReHope.Interfaces
{
    public interface ILogProdutoRepository
    {
        List<LogProduto> Listar();
        List<LogProduto> BuscarLogProdutoPorPodutoId(Guid produtoId);

    }
}
