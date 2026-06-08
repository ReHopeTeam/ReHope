namespace ReHope.Interfaces
{
    public interface IImageDescriptionRepository
    {
        // a string vai ser o tipo do retorno e o IFormFile é o tipo do que vamos passar pro código

        Task<string> CriarDescricao(IFormFile imagem);
    }
}
