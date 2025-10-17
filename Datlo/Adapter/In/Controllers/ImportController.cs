using Datlo.Application.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace Datlo.Adapter.In.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImportController(IImportFileUseCase _useCase) : ControllerBase
    {

        [HttpPost("csv")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ImportCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("O arquivo CSV é obrigatório.");
            }

            try
            {
                await _useCase.ExecuteImportAsync(file);

                return Ok(new { Message = "Importação de arquivo iniciada e concluída com sucesso (síncrona)." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao processar o arquivo: {ex.Message}");
            }
        }
    }

}
