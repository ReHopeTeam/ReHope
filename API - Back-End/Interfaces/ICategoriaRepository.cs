using ReHope.Domains;
using ReHope.DTOs.CategoriaDto;

namespace ReHope.Interfaces
{
    public interface ICategoriaRepository
    {
        List<Categoria> Listar();

        Categoria? ObterPorId(int id);

        Categoria? ObterCategoriaPorTipo(string nomeTipo);

        Categoria? BuscarPorNome(string nomeCategoria);

        bool NomeCategoriaExiste(string nome, int? categoriaIdAtual = null);

        bool TipoProdutoExiste(int idTipoProduto);

        void Adicionar (Categoria categoria);

        void Atualizar (Categoria categoria);

        void Remover (int id);

    }
}
