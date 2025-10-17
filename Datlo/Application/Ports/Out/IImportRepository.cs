using Datlo.Domain.Entities;

namespace Datlo.Application.Ports.Out
{
    public interface IImportRepository
    {
        Task SaveBatchAsync(IEnumerable<ImportedDataModel> dadosLote);
    }
}
