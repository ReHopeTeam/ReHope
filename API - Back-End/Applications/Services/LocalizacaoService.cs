using ReHope.Applications.Services;
using ReHope.Domains;
using ReHope.DTOs.CategoriaDto;
using ReHope.DTOs.LocalizacaoDto;
using ReHope.DTOs.ProdutoDto;
using ReHope.Exceptions;
using ReHope.Interfaces;

namespace ReHope.Applications.Services
{
    public class LocalizacaoService
    {
        private readonly ILocalizacaoRepository _repository;

        public LocalizacaoService(ILocalizacaoRepository repository)
        {
            _repository = repository;
        }

        public List<LerLocalizacaoDto> Listar()
        {
            List<Localizacao> localizacoes = _repository.Listar();

            List<LerLocalizacaoDto> localizacaoDto = localizacoes.Select(l => new LerLocalizacaoDto
            {
                LocalizacaoID = l.LocalizacaoID,
                NomeLocalizacao = l.NomeLocalizacao,
            }).ToList();

            return localizacaoDto;
        }

        public LerLocalizacaoDto BuscarPorID(int id)
        {
            Localizacao localizacao = _repository.BuscarPorID(id);

            if (localizacao == null)
            {
                throw new DomainException("Localização não encontrada");
            }

            LerLocalizacaoDto localizacaoDto = new LerLocalizacaoDto
            {
                LocalizacaoID = localizacao.LocalizacaoID,
                NomeLocalizacao = localizacao.NomeLocalizacao
            };
            return localizacaoDto;
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome é orbigatório");
            }
        }

        public void Adicionar(CriarLocalizacaoDto criarDto)
        {
            ValidarNome(criarDto.NomeLocalizacao);

            if (_repository.NomeExiste(criarDto.NomeLocalizacao))
            {
                throw new DomainException("Localização já existe");
            }

            Localizacao localizacao = new Localizacao
            {
                NomeLocalizacao = criarDto.NomeLocalizacao
            };

            _repository.Adicionar(localizacao);
        }

        public void Atualizar(int id, CriarLocalizacaoDto criarDto)
        {
            ValidarNome(criarDto.NomeLocalizacao);

            Localizacao localizacaoBanco = _repository.BuscarPorID(id);

            if (localizacaoBanco == null)
            {
                throw new DomainException("Localização não encontrada");
            }

            if (_repository.NomeExiste(criarDto.NomeLocalizacao, localizacaoIDAtual: id))
            {
                throw new DomainException("Já existe uma localização com esse nome");

            }
            localizacaoBanco.NomeLocalizacao = criarDto.NomeLocalizacao;
            _repository.Atualizar(localizacaoBanco);
        }
        public void Remover(int id)
        {
            Localizacao localizacaoBanco = _repository.BuscarPorID(id);
            if (localizacaoBanco == null)
            {
                throw new DomainException("Localização não encontrada");
            }
            _repository.Remover(id);
        }

    }
}



