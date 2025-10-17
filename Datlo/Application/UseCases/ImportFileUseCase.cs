using Datlo.Application.Ports.In;
using Datlo.Application.Ports.Out;

namespace Datlo.Application.UseCases
{
    public class ImportFileUseCase : IImportFileUseCase
    {
        private readonly IProcessFile _processor;
        private readonly IImportRepository _repository;

        public ImportFileUseCase(IProcessFile processor, IImportRepository repository)
        {
            _processor = processor;
            _repository = repository;
        }

        public async Task ExecuteImportAsync(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                await _processor.ProcessStreamAsync(stream, async lote => await _repository.SaveBatchAsync(lote));
            }

        }
    }
}
