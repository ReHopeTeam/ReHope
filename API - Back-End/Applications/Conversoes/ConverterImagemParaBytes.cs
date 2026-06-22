namespace ReHope.Applications.Conversoes
{
    public class ConverterImagemParaBytes
    {
        public static byte[]? ConverterImagem(IFormFile imagem)
        {
            if (imagem == null || imagem.Length == 0)
                return null;

            using var ms = new MemoryStream();
            imagem.CopyTo(ms);

            return ms.ToArray();
        }
    }
}