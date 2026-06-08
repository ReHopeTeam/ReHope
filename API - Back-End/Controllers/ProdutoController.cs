using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReHope.Applications.Services;
using ReHope.DTOs.ProdutoDto;
using ReHope.Exceptions;
using ReHope.Interfaces;
using System.Security.Claims;

namespace ReHope.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _service;

        public ProdutoController(ProdutoService service)
        {
            _service = service;
        }

        private Guid ObterUsuarioIdLogado()
        {
            string? idTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(idTexto))
            {
                throw new DomainException("Usuário não autenticado");
            }

            return Guid.Parse(idTexto);
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<LerProdutoDto>> Listar()
        {
            List<LerProdutoDto> produtos = _service.Listar();
            return Ok(produtos);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<LerProdutoDto> ObterPorId(Guid id)
        {
            try
            {
                LerProdutoDto produto = _service.ObterPorId(id);
                return Ok(produto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // para um get diferente do id, precisamos utilizar o -/-, assim o código consegue diferenciar os números
        [Authorize]
        [HttpGet("codigo/{codigo}")]
        public ActionResult<LerProdutoDto> ObterPorCodigo(int codigo)
        {
            try
            {
                LerProdutoDto produto = _service.ObterPorCodigo(codigo);
                return Ok(produto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Adicionar([FromForm] CriarProdutoDto produtoDto)
        {
            try
            {
                Guid usuarioId = ObterUsuarioIdLogado();
                int categoriaId = produtoDto.CategoriaID;
                int localizacaoId = produtoDto.LocalizacaoID;

                await _service.Adicionar(produtoDto, usuarioId, categoriaId, localizacaoId);

                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public ActionResult Atualizar(Guid id, [FromForm] AtualizarProdutoDto produtoDto)
        {
            try
            {
                _service.Atualizar(id, produtoDto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Remover(Guid id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
