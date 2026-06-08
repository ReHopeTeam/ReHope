using Microsoft.EntityFrameworkCore;
using ReHope.Applications.Regras;
using ReHope.Domains;
using ReHope.DTOs.CategoriaDto;
using ReHope.DTOs.LogProdutoDto;
using ReHope.DTOs.TipoProdutoDto;
using ReHope.Exceptions;
using ReHope.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ReHope.Applications.Services
{
    public class TipoProdutoService
    {
        private readonly ITipoProdutoRepository _repository;

        public TipoProdutoService(ITipoProdutoRepository repository)
        {
            _repository = repository;
        }

        public List<LerTipoProdutoDto> Listar()
        {
            List<TipoProduto> tiposProduto = _repository.Listar();
            List<LerTipoProdutoDto> tiposProdutoDto = tiposProduto.Select(tipoProduto => new LerTipoProdutoDto
            {
                TipoId = tipoProduto.TipoProdutoID,
                NomeTipo = tipoProduto.NomeTipo
            }).ToList();
            return tiposProdutoDto;
        }
        public LerTipoProdutoDto BuscarPorID(int tipoId)
        {
            TipoProduto tpProduto= _repository.BuscarPorID(tipoId);

            if (tpProduto == null)
            {
                throw new DomainException("Tipo de produto não encontrado.");
            }

            LerTipoProdutoDto tipoProdutoDto= new LerTipoProdutoDto
            {
                TipoId = tpProduto.TipoProdutoID,
                NomeTipo = tpProduto.NomeTipo
            };

            return tipoProdutoDto;
        }
        public List<LerCategoriaDto> ListarCategoriasPorTipo(int tipoId)
        {
            List<TipoProduto> tiposProduto = _repository.ListarCategoria(tipoId);
            List<LerCategoriaDto> categoriasDto = tiposProduto
                .Where(tp => tp.Categoria != null)
                .SelectMany(tp => tp.Categoria)
                .Select(c => new LerCategoriaDto
                {
                    CategoriaID = c.CategoriaID,
                    NomeCategoria = c.NomeCategoria
                }).ToList();

            return categoriasDto;
        }


        public void Adicionar(CriarTipoProdutoDto dto)
        {
            Validacao.ValidarNome(dto.NomeTipo);

            TipoProduto tpProdutoExiste = _repository.BuscarPorNome(dto.NomeTipo);

            if (tpProdutoExiste != null)
            {
                throw new DomainException("Já existe um tipo de produto cadastrado com esse nome.");
            }

            TipoProduto tipoProduto = new TipoProduto
            {
                NomeTipo = dto.NomeTipo
            };

            _repository.Adicionar(tipoProduto);
        }

        public void Atualizar(int tpProdutoId, CriarTipoProdutoDto dto)
        {
            Validacao.ValidarNome(dto.NomeTipo);

            TipoProduto? tpProdutoBanco = _repository.BuscarPorID(tpProdutoId);

            if (tpProdutoBanco == null)
            {
                throw new DomainException("Tipo de produto não encontrado.");
            }

            TipoProduto tpProdutoExiste = _repository.BuscarPorNome(dto.NomeTipo);

            if (tpProdutoExiste != null)
            {
                throw new DomainException("Já existe um tipo de produto cadastrado com esse nome.");
            }

            tpProdutoBanco.NomeTipo = dto.NomeTipo;

            _repository.Atualizar(tpProdutoBanco);
        }



        public void Remover(int id)
        {
            TipoProduto? tpProduto = _repository.BuscarPorID(id);

            if (tpProduto == null)
            {
                return;
            }

            _repository.Remover(id);
        }
    }
}
