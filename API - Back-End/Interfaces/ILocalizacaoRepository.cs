using ReHope.Domains;

namespace ReHope.Interfaces
{
    public interface ILocalizacaoRepository
    {
        List<Localizacao> Listar();
        Localizacao? BuscarPorID(int id);
        bool NomeExiste(string nomeLocalizacao, int? localizacaoIDAtual = null);

        void Adicionar(Localizacao localizacao);
        void Atualizar(Localizacao localizacao);
        void Remover(int id);
    }
}
