using ReHope.Exceptions;

namespace ReHope.Applications.Regras
{
    public class Validacao
    {
        public static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome é obrigatório.");
            }
        }

        public static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new DomainException("Endereço de e-mail é obrigatório.");
            }
        }

        public static void ValidarTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
            {
                throw new DomainException("Telefone é obrigatório.");
            }
        }
    }
}