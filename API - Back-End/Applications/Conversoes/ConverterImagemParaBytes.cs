namespace ReHope.Applications.Conversoes
{
    public class ConverterImagemParaBytes
    {
        public static string ConverterImagem(IFormFile imagem)
        {
            if (imagem == null || imagem.Length == 0) return null!;

            using var ms = new MemoryStream();
            imagem.CopyTo(ms);
            byte[] imagemBytes = ms.ToArray();

            return Convert.ToBase64String(imagemBytes);
        }
    }
}