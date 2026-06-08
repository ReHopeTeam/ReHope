using ReHope.Contexts;
using ReHope.Domains;
using ReHope.Interfaces;

namespace ReHope.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ReHopeContext _context;

        public UsuarioRepository(ReHopeContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.ToList();
        }

        public Usuario? ObterPorId(Guid id)
        {
            return _context.Usuario.Find(id);
        }

        public Usuario? ObterPorEmail(string email)
        {
            return _context.Usuario.FirstOrDefault(usuario => usuario.Email == email);
        }

        public Usuario? ObterPorTelefone(string telefone)
        {
            return _context.Usuario.FirstOrDefault(u => u.Telefone == telefone);
        }

        public bool EmailExiste(string email)
        {
            return _context.Usuario.Any(usuario => usuario.Email == email);
        }

        public bool TelefoneExiste(string telefone)
        {
            return _context.Usuario.Any(u => u.Telefone == telefone);
        }

        public void Adicionar (Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public void Atualizar (Usuario usuario)
        {
            Usuario? usuarioBanco = _context.Usuario.FirstOrDefault(u => u.UsuarioID == usuario.UsuarioID);

            if(usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.Email = usuario.Email;
            usuarioBanco.Senha = usuario.Senha;
            usuarioBanco.Telefone = usuario.Telefone;

            _context.SaveChanges();
        }

        public void Remover (Guid id)
        {
            Usuario? usuarioBanco = _context.Usuario.FirstOrDefault(u => u.UsuarioID == id);

            if(usuarioBanco == null)
            {
                return;
            }

            _context.Usuario.Remove(usuarioBanco);
            _context.SaveChanges();
        }

    }
}
