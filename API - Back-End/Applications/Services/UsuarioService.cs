using Microsoft.EntityFrameworkCore;
using ReHope.Applications.Regras;
using ReHope.Domains;
using ReHope.DTOs.UsuarioDto;
using ReHope.Exceptions;
using ReHope.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ReHope.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<LerUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<LerUsuarioDto> usuariosDto = usuarios.Select(usuario => new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                StatusUsuario = usuario.StatusUsuario
            }).ToList();

            return usuariosDto;
        }

        public LerUsuarioDto BuscarPorId(Guid usuarioId)
        {
            Usuario usuario = _repository.ObterPorId(usuarioId);

            if(usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            LerUsuarioDto usuarioDto = new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                StatusUsuario = usuario.StatusUsuario
            };

            return usuarioDto;
        }

        public LerUsuarioDto BuscarPorEmail(string email)
        {
            Usuario usuario = _repository.ObterPorEmail(email);

            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            LerUsuarioDto usuarioDto = new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                StatusUsuario = usuario.StatusUsuario
            };

            return usuarioDto;
        }

        private static byte[] HashSenha (string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new DomainException("Senha é obrigatória.");
            }

            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public void Adicionar (CriarUsuarioDto usuarioDto)
        {
            Validacao.ValidarNome(usuarioDto.Nome);
            Validacao.ValidarEmail(usuarioDto.Email);
            Validacao.ValidarTelefone(usuarioDto.Telefone);

            if(_repository.EmailExiste(usuarioDto.Email))
            {
                throw new DomainException("Já existe um usuário com esse e-mail.");
            }

            if (_repository.TelefoneExiste(usuarioDto.Telefone))
            {
                throw new DomainException("Já existe um usuário com esse telefone.");
            }

            Usuario usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = HashSenha(usuarioDto.Senha),
                Telefone = usuarioDto.Telefone
            };

            _repository.Adicionar(usuario); 
        }

        public void Atualizar (Guid usuarioId, CriarUsuarioDto usuarioDto)
        {
             Usuario usuarioBanco = _repository.ObterPorId(usuarioId);

            if(usuarioBanco == null)
            {
                throw new DomainException("Usuário não enontrado.");
            }
            Validacao.ValidarNome(usuarioDto.Nome);
            Validacao.ValidarEmail(usuarioDto.Email);
            Validacao.ValidarTelefone(usuarioDto.Telefone);

            Usuario usuarioComMesmoEmail = _repository.ObterPorEmail(usuarioDto.Email);

            if(usuarioComMesmoEmail != null && usuarioComMesmoEmail.UsuarioID != usuarioId)
            {
                throw new DomainException("Já existe um usuário com esse e-mail.");
            }

            Usuario usuarioComMesmoTelefone = _repository.ObterPorTelefone(usuarioDto.Telefone);

            if(usuarioComMesmoTelefone != null && usuarioComMesmoTelefone.UsuarioID != usuarioId)
            {
                throw new DomainException("Já existe um usuário com esse telefone.");
            }

            usuarioBanco.Nome = usuarioDto.Nome;
            usuarioBanco.Email = usuarioDto.Email;
            usuarioBanco.Telefone = usuarioDto.Telefone;
            usuarioBanco.Senha = HashSenha(usuarioDto.Senha);

            _repository.Atualizar(usuarioBanco);
        }

        public void Remover(Guid id)
        {
            Usuario usuario = _repository.ObterPorId(id);

            if(usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            _repository.Remover(id);
        }
    }
}
