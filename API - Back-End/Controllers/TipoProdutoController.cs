using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReHope.Applications.Services;
using ReHope.DTOs.ProdutoDto;
using ReHope.DTOs.TipoProdutoDto;
using ReHope.Exceptions;
using System.Security.Claims;

namespace ReHope.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProdutoController : ControllerBase
    {
        private readonly TipoProdutoService _service;
        public TipoProdutoController(TipoProdutoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<LerTipoProdutoDto>> Listar()
        {
            List<LerTipoProdutoDto> tipos = _service.Listar();
            return Ok(tipos);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<LerTipoProdutoDto> BuscarPorID(int id)
        {
            try
            {
                LerTipoProdutoDto tpProduto = _service.BuscarPorID(id);
                return Ok(tpProduto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Adicionar(CriarTipoProdutoDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Atualizar(int id, CriarTipoProdutoDto dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Remover(int id)
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