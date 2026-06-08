using ReHope.Domains;

namespace ReHope.Interfaces
{
    public interface IUsuarioRepository
    {

        List<Usuario> Listar();

        Usuario? ObterPorId(Guid id);

        Usuario? ObterPorEmail(string email);

        Usuario? ObterPorTelefone(string telefone);

        bool EmailExiste(string email);

        bool TelefoneExiste(string telefone);

        void Adicionar (Usuario usuario);

        void Atualizar (Usuario usuario);

        void Remover (Guid  id);
    }
}
