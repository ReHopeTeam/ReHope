using ReHope.Domains;

namespace ReHope.Interfaces
{
    public interface ITipoProdutoRepository
    {
        List<TipoProduto> Listar();
        List<TipoProduto> ListarCategoria(int tipoId);
        TipoProduto BuscarPorNome(string nomeTipo);
        TipoProduto BuscarPorID(int tipoId);
        void Adicionar(TipoProduto tipoProduto);
        void Atualizar(TipoProduto tipoProduto);
        void Remover(int id);
    }
}
