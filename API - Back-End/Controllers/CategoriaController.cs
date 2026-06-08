using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReHope.Applications.Services;
using ReHope.DTOs.CategoriaDto;
using ReHope.DTOs.TipoProdutoDto;
using ReHope.DTOs.UsuarioDto;
using ReHope.Exceptions;

namespace ReHope.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _service;

        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<LerCategoriaDto>> Listar()
        {
            List<LerCategoriaDto> categoriaDto = _service.Listar();
            return Ok(categoriaDto);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<LerCategoriaDto> ObterPorId(int id)
        {
            try
            {
                LerCategoriaDto categoriaDto = _service.ObterPorID(id);
                return Ok(categoriaDto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [Authorize]
        [HttpGet("nomeCategoria/{nomeCategoria}")]
        public ActionResult<LerCategoriaDto> BuscarPorNome(string nomeCategoria)
        {
            try
            {
                LerCategoriaDto categoria = _service.BuscarPorNome(nomeCategoria);
                return Ok(categoria);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Authorize]
        [HttpPost]
        public ActionResult Adicionar(CriarCategoriaDto categoriaDto)
        {
            try
            {
                 _service.Adicionar(categoriaDto);
                 return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Atualizar(int id, CriarCategoriaDto categoriaDto)
        {
            try
            {
                _service.Atualizar(id, categoriaDto);
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
