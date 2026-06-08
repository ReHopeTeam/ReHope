namespace ReHope.Interfaces
{
    public interface IContentSafetyRepository
    {
        Task<(bool aprovado, string msg)> ValidarConteudo(string texto);
    }
}
