using Google.GenAI;
using Google.GenAI.Types;
using ReHope.Interfaces;

namespace ReHope.Applications.ImageDescription
{
    public class ImageDescriptionService : IImageDescriptionRepository
    {
        private readonly string _apiKey;

        public ImageDescriptionService(IConfiguration configuration)
        {
            _apiKey = configuration["Gemini:ApiKey"] ??
                      System.Environment.GetEnvironmentVariable("GEMINI_API_KEY") ??
                      throw new Exception("API key não configurada.");
        }

        public async Task<string> CriarDescricao(IFormFile imagem)
        {
            if (imagem == null || imagem.Length == 0)
            {
                throw new Exception("Nenhuma imagem foi enviada.");
            }

            try
            {
                using var memoryStream = new MemoryStream();
                await imagem.CopyToAsync(memoryStream);

                byte[] imageBytes = memoryStream.ToArray();

                var client = new Client(apiKey: _apiKey);

                string prompt = """
                Você é um especialista em catalogação de produtos para bazares e brechós.

                Analise a imagem enviada e gere uma descrição para o produto.

                Regras:
                - Descreva apenas características visíveis.
                - Não invente marca.
                - Não invente tamanho.
                - Não invente material.
                - Destaque cores, estampas e estilo.
                - Linguagem adequada para e-commerce.
                - Responda somente com a descrição.

                Exemplo:
                Vestido feminino azul estampado com mangas curtas e modelagem casual, ideal para uso no dia a dia.
                """;

                Content conteudo = new Content
                {
                    Role = "user",
                    Parts =
                    [
                        new Part
                        {
                            Text = prompt
                        },
                        new Part
                        {
                            InlineData = new Blob
                            {
                                MimeType = imagem.ContentType,
                                Data = imageBytes
                            }
                        }
                    ]
                };

                GenerateContentResponse response =
                    await client.Models.GenerateContentAsync(
                        model: "gemini-2.0-flash",
                        contents: conteudo
                    );

                return response.Text ??
                       "Não foi possível gerar uma descrição.";
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao gerar descrição: {ex.Message}");
            }
        }
    }
}