using ReHope.Applications.Regras;
using ReHope.Domains;
using ReHope.DTOs.CategoriaDto;
using ReHope.DTOs.TipoProdutoDto;
using ReHope.DTOs.UsuarioDto;
using ReHope.Exceptions;
using ReHope.Interfaces;
using ReHope.Repository;

namespace ReHope.Applications.Services
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _repository;
        private readonly ITipoProdutoRepository _repositoryTipoProduto;

        public CategoriaService(ICategoriaRepository repository, ITipoProdutoRepository repositoryTipoProduto)
        {
            _repository = repository;
            _repositoryTipoProduto = repositoryTipoProduto;
        }

        public List<LerCategoriaDto> Listar()
        {
            List<Categoria> categorias = _repository.Listar();

            List<LerCategoriaDto> categoriaDto = categorias.Select(categoria => new LerCategoriaDto
            {
                CategoriaID = categoria.CategoriaID,
                NomeCategoria = categoria.NomeCategoria,
                TipoProdutoID = categoria.TipoProdutoID,
                NomeTipo = categoria.TipoProduto.NomeTipo
            }).ToList();

            return categoriaDto;
        }

        public LerCategoriaDto ObterPorID(int id)
        {
            Categoria categoria = _repository.ObterPorId(id)!;
            if (categoria == null)
            {
                throw new DomainException("Categoria não encontrada!");
            }

            LerCategoriaDto categoriaDto = new LerCategoriaDto
            {
                CategoriaID = categoria.CategoriaID,
                NomeCategoria = categoria.NomeCategoria,
                TipoProdutoID = categoria.TipoProdutoID,
                NomeTipo = categoria.TipoProduto.NomeTipo
            };

            return categoriaDto;
        }

        public LerCategoriaDto ObterCategoriaPorTipo(string nomeTipo)
        {
            Categoria categoria = _repository.ObterCategoriaPorTipo(nomeTipo)!;

            if (categoria == null)
            {
                throw new DomainException("Tipo Produto não encontrado!");
            }

            LerCategoriaDto categoriaDto = new LerCategoriaDto
            {
                CategoriaID = categoria.CategoriaID,
                NomeCategoria = categoria.NomeCategoria,
                TipoProdutoID = categoria.TipoProdutoID,
                NomeTipo = categoria.TipoProduto.NomeTipo
            };

            return categoriaDto;
        }

        public LerCategoriaDto BuscarPorNome(string nomeCategoria)
        {
            Categoria categoria = _repository.BuscarPorNome(nomeCategoria)!;

            if (categoria == null)
            {
                throw new DomainException("Categoria não encontrado.");
            }

            LerCategoriaDto categoriaDto = new LerCategoriaDto
            {
               CategoriaID = categoria.CategoriaID,
               NomeCategoria= categoria.NomeCategoria,
               TipoProdutoID = categoria.TipoProdutoID,
               NomeTipo = categoria.TipoProduto.NomeTipo
            };

            return categoriaDto;
        }

        public void Adicionar(CriarCategoriaDto categoriaDto)
        {
            Validacao.ValidarNome(categoriaDto.NomeCategoria);

            Categoria categoriaExiste = _repository.BuscarPorNome(categoriaDto.NomeCategoria)!;
            if (categoriaExiste != null)
                throw new DomainException("Já existe uma categoria cadastrada com esse nome.");
            //condicional para validar se o tp produto existe

            //TipoProduto tipoProdutoExiste = _repositoryTipoProduto.BuscarPorID(tipoProdutoDto.TipoId);

            //if( tipoProdutoExiste != null)
            //{
            //    throw new DomainException("Esse tipo produto não existe.");
            //}

            if (!_repository.TipoProdutoExiste(categoriaDto.TipoProdutoID))
            {
                throw new DomainException("Esse tipo produto não existe.");
            }



            Categoria categoria = new Categoria
            {
                NomeCategoria = categoriaDto.NomeCategoria,
                TipoProdutoID = categoriaDto.TipoProdutoID
            };

            _repository.Adicionar(categoria);
        }

        public void Atualizar(int categoriaId, CriarCategoriaDto categoriaDto)
        {
            Validacao.ValidarNome(categoriaDto.NomeCategoria);

            Categoria? categoriaBanco = _repository.ObterPorId(categoriaId);

            if (categoriaBanco == null)
            {
                throw new DomainException("Categoria não encontrada!");
            }

            Categoria? categoriaExiste = _repository.BuscarPorNome(categoriaDto.NomeCategoria);

            if (categoriaExiste != null)
            {
                throw new DomainException("Já existe um tipo de produto cadastrado com esse nome.");
            }

            categoriaBanco.NomeCategoria = categoriaDto.NomeCategoria;

            _repository.Atualizar(categoriaBanco);
        }



        public void Remover(int categoriaId)
        {
            Categoria? categoria = _repository.ObterPorId(categoriaId);

            if (categoria == null)
            {
                return;
            }

            _repository.Remover(categoriaId);
        }
    }
}

