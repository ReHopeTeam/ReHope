using ReHope.Domains;

namespace ReHope.Interfaces
{
    public interface IProdutoRepository
    {
        List<Produto> Listar();
        Produto ObterPorId(Guid id);
        Produto ObterPorCodigo(int codigo);
        bool LocalizacaoExiste(int localizacaoId);
        void Adicionar(Produto produto);    
        void Atualizar(Produto produto);
        void Remover(Guid id);
        Localizacao BuscarLocalizacaoPorNome(string nomeLocalizacao);

    }
}
