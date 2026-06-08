namespace ReHope.DTOs.UsuarioDto
{
    public class LerUsuarioDto
    {
        public Guid UsuarioID { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty ;

        public string Telefone {  get; set; } = string.Empty ;
        public Boolean? StatusUsuario { get; set;}
    }
}
