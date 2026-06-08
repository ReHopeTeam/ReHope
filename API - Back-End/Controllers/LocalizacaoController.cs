using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReHope.Applications.Services;
using ReHope.DTOs.CategoriaDto;
using ReHope.DTOs.LocalizacaoDto;
using ReHope.Exceptions;

namespace ReHope.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        private readonly LocalizacaoService _service;
        public LocalizacaoController(LocalizacaoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<LerLocalizacaoDto>> Listar()
        {
            List<LerLocalizacaoDto> localizacoes = _service.Listar();
            return Ok(localizacoes);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<LerLocalizacaoDto> BuscarPorID(int id)
        {
            LerLocalizacaoDto? localizacoes = _service.BuscarPorID(id);
            if (localizacoes == null)
            {
                return NotFound();
            }
            return Ok(localizacoes);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Adicionar(CriarLocalizacaoDto criarDto)
        {
            try
            {
                _service.Adicionar(criarDto);
                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Atualizar(int id, CriarLocalizacaoDto criarDto)
        {
            try
            {
                _service.Atualizar(id, criarDto);
                return StatusCode(204);
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
                return StatusCode(204);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
