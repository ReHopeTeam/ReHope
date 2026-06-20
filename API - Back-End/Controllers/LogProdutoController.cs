using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReHope.Applications.Services;
using ReHope.DTOs.LogProdutoDto;
using ReHope.Exceptions;

namespace ReHope.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogProdutoController : ControllerBase
    {
        private readonly LogProdutoService _service;

        public LogProdutoController(LogProdutoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<LerLogProdutoDto>> Listar()
        {
            List<LerLogProdutoDto> logProdutoDtos = _service.Listar();
            return Ok(logProdutoDtos);
        }


        [Authorize]
        [HttpGet("produto/")]
        public ActionResult<List<LerLogProdutoDto>> BuscarLogProdutoPorPodutoId(Guid produtoId)
        {
            try
            {
                List<LerLogProdutoDto> logProdutoDtos = _service.BuscarLogProdutoPorPodutoId(produtoId);
                return Ok(logProdutoDtos);

            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
